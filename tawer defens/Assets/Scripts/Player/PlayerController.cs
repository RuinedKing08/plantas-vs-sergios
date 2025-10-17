using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Canvas buildMenuCanvas;
    [SerializeField] private Button[] towerButtons;
    [SerializeField] private TMP_Text resourceText;

    [Header("Tower Prefabs & Costs")]
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private int[] towerCosts;

    [Header("Placement Settings")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Material validMat;
    [SerializeField] private Material invalidMat;
    [SerializeField] private float yOffset = 0.5f;

    private Camera mainCamera;
    private GameObject towerGhost;
    private GameObject towerToPlace;
    private int currentCost;
    private bool isPlacing;

    private void Start()
    {
        mainCamera = Camera.main;

        if (buildMenuCanvas != null)
            buildMenuCanvas.gameObject.SetActive(true);

        for (int i = 0; i < towerButtons.Length && i < towerPrefabs.Length; i++)
        {
            int index = i; 
            towerButtons[i].onClick.AddListener(() => StartPlacement(towerPrefabs[index], towerCosts[index]));
        }
    }

    private void Update()
    {
        UpdateResourceUI();

        if (!isPlacing) return;
        HandlePlacement();
    }

    private void UpdateResourceUI()
    {
        if (resourceText != null)
        {
            resourceText.text = $"$ {Game.Instance.money}";
        }
    }

    private void StartPlacement(GameObject prefab, int cost)
    {
        if (!Game.Instance.HasResources(cost))
        {
            Debug.Log("Not enough money!");
            return;
        }

        towerToPlace = prefab;
        currentCost = cost;

        if (towerGhost != null)
            Destroy(towerGhost);

        towerGhost = Instantiate(towerToPlace);
        var tower = towerGhost.GetComponent<BaseTower>();
        if (tower != null) tower.isPreview = true;
        SetGhostMaterial(validMat);

        isPlacing = true;
    }

    private void HandlePlacement()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            Vector3 pos = hit.point;
            pos.y = yOffset;
            towerGhost.transform.position = pos;

            bool valid = IsPlacementValid(hit.point, towerGhost);
            SetGhostMaterial(valid ? validMat : invalidMat);

            if (Input.GetMouseButtonDown(0) && valid)
            {
                ConfirmPlacement(hit.point);
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacement();
            }
        }
    }

    private void ConfirmPlacement(Vector3 position)
    {
        if (towerToPlace == null) return;

        Game.Instance.SpendResources(currentCost);
        GameObject placed = Instantiate(towerToPlace, position, Quaternion.identity);
        var tower = placed.GetComponent<BaseTower>();
        tower.isPreview = false;
        Destroy(towerGhost);
        towerGhost = null;
        isPlacing = false;
    }

    private void CancelPlacement()
    {
        if (towerGhost != null)
            Destroy(towerGhost);
        isPlacing = false;
    }

    private bool IsPlacementValid(Vector3 position, GameObject ghost)
    {
        Collider[] hits = Physics.OverlapSphere(position, 0.75f);
        foreach (var hit in hits)
        {
            if (ghost != null && hit.transform.IsChildOf(ghost.transform))
                continue;

            if (hit.CompareTag("Tower") || hit.CompareTag("Enemy") || hit.CompareTag("Path"))
                return false;
        }
        return true;
    }

    private void SetGhostMaterial(Material mat)
    {
        foreach (var r in towerGhost.GetComponentsInChildren<Renderer>())
        {
            r.material = mat;
        }
    }
}