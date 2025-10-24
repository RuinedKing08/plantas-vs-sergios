using UnityEngine;

public abstract class BaseEnemy : BaseUnit, IDamageable
{
    [SerializeField] private Health health;


    public bool IsAlive => health.IsAlive;


    public void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
    }


    protected virtual void Update()
    {
        Move(Vector3.forward);
    }
}