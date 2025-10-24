using System.Net;
using System.Collections;
using UnityEngine;

public class FreezeBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float lifetime = 5f;

    private float damage;
    private Transform target;

    public void Initialize(float dmg)
    {
        damage = dmg;
        Destroy(gameObject, lifetime);
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

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent(out BaseEnemy enemy))
        {
            enemy.TakeDamage(damage);
            StartCoroutine(ApplySlow(enemy));
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplySlow(BaseEnemy enemy)
    {
        float originalSpeed = enemy.Speed;
        enemy.Speed = originalSpeed * slowAmount;
        yield return new WaitForSeconds(slowDuration);
        enemy.Speed = originalSpeed;
    }
}
