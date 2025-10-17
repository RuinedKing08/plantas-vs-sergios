using UnityEngine;

public class Tack : MonoBehaviour
{
    private float damage;
    private float lifetime;

    public void Initialize(float dmg, float range)
    {
        damage = dmg;
        lifetime = range / 10f; 
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
