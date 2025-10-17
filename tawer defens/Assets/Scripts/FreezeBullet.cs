using UnityEngine;

public class FreezeBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private float damage;
    private Transform target;
    
    public void Initialize(float ds)
    {
        damage = ds;
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }



    }

    private void ShootEnemy()
    {
        BaseEnemy enemy = target.GetComponent<BaseEnemy>();
        if (enemy != null) 
        {
            enemy.TakeDamage(damage);
            enemy.Slow(slowAmount, slowDuration);
        }
        Destroy(gameObject);
    }
}
