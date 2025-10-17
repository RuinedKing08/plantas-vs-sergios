using System.Linq;
using UnityEngine;

public class FreezeTower : BaseTower
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float fireCountdown = 1f;

    public delegate void ShootAction();
    private ShootAction towershoot;

    private void Start()
    {
        towershoot += () => Debug.Log("La torre disparó");
    }

    private void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 0f)
        {
            GameObject enemy = DetectEnemy();
            if (enemy != null)
            {
                Shoot(enemy);
                fireCountdown = 1f;
            }
        }
    }

    private GameObject DetectEnemy()
    {

        return null;
    }

    private void Shoot(GameObject enemy)
    {
        GameObject freezeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        FreezeBullet bullet = freezeBullet.GetComponent<FreezeBullet>();
        
        if (bullet != null)
        {
            bullet.SetTarget(enemy);
        }

        towershoot?.Invoke();
    }
}
