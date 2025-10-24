using UnityEngine;

public abstract class BaseUnit : MonoBehaviour, IUnit
{
    [SerializeField] private int id;
    [SerializeField] protected float moveSpeed = 3f;


    public int Id => id;


    public virtual void Initialize() { }


}
