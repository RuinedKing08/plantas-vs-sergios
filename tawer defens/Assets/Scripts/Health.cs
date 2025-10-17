using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth=> maxHealth;
    public bool IsAlive => currentHealth > 0f;


    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;
        currentHealth -= amount;


        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            OnDeath();
        }
    }


    private void OnDeath()
    {
        Destroy(gameObject);
    }


    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

}
