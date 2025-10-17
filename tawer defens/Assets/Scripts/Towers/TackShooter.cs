using UnityEngine;

public class TackShooterTower : BaseTower
{
    [Header("Tack Shooter Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int tackCount = 8;
    [SerializeField] private float range = 3f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float baseDamage = 5f;

    private float fireTimer;

    public override void Initialize()
    {
        fireTimer = 0f;
    }

    protected override void Update()
    {
        if (isPreview) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;

            if (InRange())
                ShootTacks();
        }

        base.Update();
    }

    private void ShootTacks()
    {
        float angleStep = 360f / tackCount;

        for (int i = 0; i < tackCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            GameObject proj = Instantiate(projectilePrefab, transform.position + dir * 0.5f, Quaternion.identity);
            if (proj.TryGetComponent(out Rigidbody rb))
                rb.linearVelocity = dir * projectileSpeed;

            if (proj.TryGetComponent(out Tack tack))
                tack.Initialize(GetDamage(), range);
        }
    }

    private bool InRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
                return true;
        }
        return false;
    }

    private float GetDamage()
    {
        return baseDamage * Mathf.Pow(1.2f, Level - 1);
    }

    public override void Upgrade()
    {
        base.Upgrade();
        fireRate += 0.25f;
        tackCount += 2;
        baseDamage *= 1.2f;
        range += 0.3f;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
