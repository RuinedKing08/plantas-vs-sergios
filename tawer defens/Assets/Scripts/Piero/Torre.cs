using UnityEngine;

public class Torre : BaseTower
{
    [Header("Shooter Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float range = 15f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float baseDamage = 10f;

    private float fireTimer = 0f;
    private Transform currentTarget;

    protected override void Update()
    {
        if (isPreview) return; 

        FindTarget();

        if (currentTarget != null)
        {
            RotateToTarget();

            fireTimer += Time.deltaTime;
            if (fireTimer >= 1f / fireRate)
            {
                fireTimer = 0f;
                Shoot();
            }
        }

        base.Update();
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform target = null;
        float first = -Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance > range) continue; 

            if (enemy.TryGetComponent(out BaseEnemy baseEnemy))
            {
                float progress = baseEnemy.PathProgress();
                if (progress > first)
                {
                    first = progress;
                    target = enemy.transform;
                }
            }
        }

        currentTarget = target;
    }

    private void RotateToTarget()
    {
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * 5f);
    }

    private void Shoot()
    {
        if (currentTarget == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (bullet.TryGetComponent(out Bala bala))
        {
            bala.Initialize(currentTarget, GetDamage(), range);
        }
    }

    private float GetDamage()
    {
        return baseDamage * Mathf.Pow(1.25f, Level - 1);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        baseDamage *= 1.2f;
        range += 1f;
        fireRate += 0.2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}