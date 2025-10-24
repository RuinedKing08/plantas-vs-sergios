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

    private float fireCountdown;
    private Transform target;

    public delegate void ShootAction();
    private event ShootAction towershoot;

    protected override void Start()
    {
        base.Start();
        fireCountdown = 1f / fireRate;
    }

    protected override void Update()
    {
        FindTarget();
        fireCountdown -= Time.deltaTime;

        if (target != null && fireCountdown <= 0f)
        {
            Shoot(target);
            fireCountdown = 1f / fireRate;
        }

        base.Update();
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform firstEnemy = null;
        float bestProgress = -Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance > range) continue;

            if (enemy.TryGetComponent(out BaseEnemy baseEnemy))
            {
                float progress = baseEnemy.PathProgress();
                if (progress > bestProgress)
                {
                    bestProgress = progress;
                    firstEnemy = enemy.transform;
                }
            }
        }

        target = firstEnemy;
    }

    private void Shoot(Transform enemy)
    {
        GameObject freezeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (freezeBullet.TryGetComponent(out FreezeBullet bullet))
        {
            bullet.Initialize(damage);
            bullet.SetTarget(enemy);
        }

        towershoot?.Invoke();
    }
}