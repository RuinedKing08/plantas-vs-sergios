using UnityEngine;

public abstract class BaseEnemy : BaseUnit, IDamageable
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int reward = 10;
    [SerializeField] private Transform[] path;

    private int currentWaypoint = 0;
    protected Health health;

    public bool IsAlive => health.IsAlive;
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    protected virtual void Update()
    {
        MoveAlongPath();   
    }
    public void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
        if(health.CurrentHealth <= 0)
        {
            Game.Instance.AddResources(reward);
            Destroy(gameObject);
        }
    }

    protected void MoveAlongPath()
    {
        if (path == null || path.Length == 0) return;
        if (currentWaypoint >= path.Length)
        {
            ReachGoal();
            return;
        }

        Transform target = path[currentWaypoint];
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentWaypoint++;
        }
    }

    private void ReachGoal()
    {
        Game.Instance.LoseLife();
        Destroy(gameObject);
    }
    public float PathProgress()
    {

         if (path == null || path.Length == 0) return 0f;


         float progress = currentWaypoint;
         if (currentWaypoint < path.Length)
         {
            float segmentDist = Vector3.Distance(transform.position, path[currentWaypoint].position);
            progress += 1f - Mathf.Clamp01(segmentDist / 10f); 
         }
         return progress;
    }
}
