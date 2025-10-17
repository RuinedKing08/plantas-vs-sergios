using UnityEngine;

public class FreezeBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private float damage;
    private GameObject target;
    
    public void Initialize(float ds)
    {
        damage = ds;
    }

    public void SetTarget(GameObject enemy)
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
        RageEnemy enemy = target.GetComponent<RageEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.Slow(slowAmount, slowDuration);
        }
        Destroy(gameObject);
    }
}
