using UnityEngine;
public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    [SerializeField] private Transform[] waypoints;

    public Transform[] Waypoints => waypoints;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            Gizmos.DrawSphere(waypoints[i].position, 0.2f);
        }
    }
}
