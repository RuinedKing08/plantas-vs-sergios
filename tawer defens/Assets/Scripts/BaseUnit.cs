using UnityEngine;

public abstract class BaseUnit : MonoBehaviour, IUnit
{
    [SerializeField] private int id;
    [SerializeField] protected float moveSpeed = 3f;


    public int Id => id;


    public virtual void Initialize() { }


    protected void Move(Vector3 direction)
    {
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }
}
