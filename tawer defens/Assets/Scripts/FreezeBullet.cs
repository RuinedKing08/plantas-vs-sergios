using System.Net;
using System.Collections;
using UnityEngine;

public class FreezeBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private float damage;
    private Transform target;
    
    public void Initialize(float dmg)
    {
        damage = dmg;
        //target = enemy;
        //Destroy(gameObject, lifetime);
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Health health))
        {   
            BaseEnemy enemy = target.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                StartCoroutine(ApplySlow(enemy));
            }
            Destroy(gameObject);
        }
    }

    private void ShootEnemy()
    {
        BaseEnemy enemy = target.GetComponent<BaseEnemy>();
        if (enemy != null) 
        {
            enemy.TakeDamage(damage);
            //enemy.Slow(slowAmount, slowDuration);
        }
        Destroy(gameObject);
    }

    private IEnumerator ApplySlow(BaseEnemy enemy)
    {
        //float originalSpeed = enemy.Speed;
        //enemy.Speed *= slowAmount;

        yield return new WaitForSeconds(slowDuration);

        //enemy.Speed = originalSpeed;
    }
}
