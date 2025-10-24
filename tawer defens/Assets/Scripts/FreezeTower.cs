using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class FreezeTower : BaseTower
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float fireCountdown = 1f;
    private Transform target;

    public delegate void ShootAction();
    private event ShootAction towershoot;

    private void Start()
    {
        towershoot += () => Debug.Log("La torre disparó");
    }

    private void Update()
    {
        FindTarget();
        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 0f)
        {
            if (target != null)
            {
                Shoot(target);
                fireCountdown = 1f;
            }
        }
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform target1 = null;
        float first = -Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance > range) continue; 

            if (enemy.TryGetComponent(out BaseEnemy baseEnemy))
            {
                //float progress = baseEnemy.PathProgress();
                //if (progress > first)
                //{
                //    first = progress;
                //    target1 = enemy.transform;
                //}
            }
        }

        target = target1;
    }

    private void Shoot(Transform enemy)
    {
        GameObject freezeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        FreezeBullet bullet = freezeBullet.GetComponent<FreezeBullet>();
        bullet.Initialize(damage);
        
        if (bullet != null)
        {
            bullet.SetTarget(enemy);
        }

        towershoot?.Invoke();
    }
}
