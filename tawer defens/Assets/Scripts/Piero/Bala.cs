using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float speed = 25f;     
    [SerializeField] private float turnSpeed = 10f;  
    [SerializeField] private float lifetime = 4f;      

    private Transform target;
    private float damage;
    private Vector3 startPosition;
    private float maxDistance;

    public void Initialize(Transform targetTransform, float dmg, float range)
    {
        target = targetTransform;
        damage = dmg;
        maxDistance = range;
        startPosition = transform.position;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
            Destroy(gameObject);
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
