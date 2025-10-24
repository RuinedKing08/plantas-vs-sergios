using UnityEngine;

public class RageEnemy2 : BaseEnemy
{
    [Header("Rage Settings")]
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private Color rageColor = Color.blue;
    [SerializeField] private Renderer bodyRenderer;

    [Header("Scale Settings")]
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private float scaleSpeed = 3f;

    private Vector3 targetScale;
    private float nextScaleChangeTime;

    public override void Initialize()
    {
        base.Initialize();
        if (bodyRenderer == null)
            bodyRenderer = GetComponentInChildren<Renderer>();
        targetScale = transform.localScale;
        nextScaleChangeTime = Time.time + Random.Range(2f, 3f);
    }

    protected override void Update()
    {
        float healthPercent = Mathf.Clamp01(health.CurrentHealth / health.MaxHealth);
        float rageFactor = 1f - healthPercent;
        speed = Mathf.Lerp(baseSpeed, maxSpeed, rageFactor);

        if (bodyRenderer != null)
        {
            Color newColor = Color.Lerp(Color.white, rageColor, rageFactor);
            bodyRenderer.material.color = newColor;
        }

        if (Time.time >= nextScaleChangeTime)
        {
            float randomScale = Random.Range(minScale, maxScale);
            targetScale = Vector3.one * randomScale;
            nextScaleChangeTime = Time.time + Random.Range(2f, 3f);
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        base.Update();
    }

    public override void TakeDamage(float amount)
    {
        float scaleFactor = transform.localScale.x;
        float damageModifier = Mathf.Lerp(1.5f, 0.5f, (scaleFactor - minScale) / (maxScale - minScale));
        base.TakeDamage(amount * damageModifier);
    }
}
