using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public abstract class BaseTower : MonoBehaviour, IUpgradable
{
    [Header("Tower Info")]
    [SerializeField] private int id;
    [SerializeField] private int level = 1;
    [SerializeField] private float baseCost = 25f;

    [Header("Upgrade Settings")]
    [SerializeField] private float upgradeBaseCost = 50f;
    [SerializeField] private float upgradeMultiplier = 1.5f;
    [SerializeField] private int maxLevel = 5;

    [Header("UI References")]
    [SerializeField] private Canvas upgradeCanvas;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text levelText;

    private bool isSelected;
    public bool isPreview;

    public int Id => id;
    public int Level => level;
    public float Cost => baseCost;


    public virtual void Initialize() { }

    protected virtual void Start()
    {
        if (upgradeCanvas != null)
            upgradeCanvas.gameObject.SetActive(false);

        if (upgradeButton != null)
            upgradeButton.onClick.AddListener(AttemptUpgrade);

        UpdateUI();
    }

    protected virtual void Update()
    {
        if (isPreview) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                    ToggleUpgradeUI(true);
                else if (isSelected)
                    ToggleUpgradeUI(false);
            }
        }
    }

    private void ToggleUpgradeUI(bool state)
    {
        isSelected = state;
        if (upgradeCanvas != null)
            upgradeCanvas.gameObject.SetActive(state);

        UpdateUI();
    }

    private void AttemptUpgrade()
    {
        if (Level >= maxLevel)
        {
            return;
        }

        int cost = GetUpgradeCost();
        if (!Game.Instance.HasResources(cost))
        {
            Debug.Log("Not enough resources!");
            return;
        }

        Game.Instance.SpendResources(cost);
        Upgrade();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (upgradeCanvas == null) return;

        if (levelText != null)
            levelText.text = $"Lvl {Level}";

        if (costText != null)
        {
            if (Level >= maxLevel)
                costText.text = "MAX";
            else
                costText.text = $"${GetUpgradeCost()}";
        }

        if (upgradeButton != null)
            upgradeButton.interactable = Level < maxLevel;
    }

    public int GetUpgradeCost()
    {
        if (Level >= maxLevel) return 0;
        return Mathf.FloorToInt(upgradeBaseCost * Mathf.Pow(upgradeMultiplier, Level - 1));
    }

    public virtual void Upgrade()
    {
        level++;
        Debug.Log($"{name} upgraded to level {level}");
    }
}
