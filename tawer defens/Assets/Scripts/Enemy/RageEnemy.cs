using UnityEngine;

public class RageEnemy : BaseEnemy
{
    [Header("Rage Settings")]
    [SerializeField] private float baseSpeed = 2f;    
    [SerializeField] private float maxSpeed = 8f;   
    [SerializeField] private Color rageColor = Color.red;
    [SerializeField] private Renderer bodyRenderer;
    public override void Initialize()
    {
        base.Initialize();

        if (bodyRenderer == null)
            bodyRenderer = GetComponentInChildren<Renderer>();
    }

    protected override void Update()
    {
        if (health == null) return;

        float healthPercent = Mathf.Clamp01(health.CurrentHealth / health.MaxHealth);
        float rageFactor = 1f - healthPercent; 

        moveSpeed = Mathf.Lerp(baseSpeed, maxSpeed, rageFactor);

        if (bodyRenderer != null)
        {
            Color newColor = Color.Lerp(Color.white, rageColor, rageFactor);
            bodyRenderer.material.color = newColor;
        }

        base.Update();
    }

}