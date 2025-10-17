using UnityEngine;

public class ItemCash : MonoBehaviour
{
    private int value = 1;

    public void Initialize(int moneyValue)
    {
        value = moneyValue;
    }

    private void OnMouseDown()
    {
        Game.Instance.AddResources(value);
        Destroy(gameObject);
    }
}
