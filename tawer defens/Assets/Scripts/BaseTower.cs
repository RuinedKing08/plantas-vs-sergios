using UnityEngine;

public abstract class BaseTower : MonoBehaviour, IUpgradable
{
    [SerializeField] private int id;
    [SerializeField] private int level = 1;
    [SerializeField] private float cost = 25f;


    public int Id => id;
    public int Level => level;
    public float Cost => cost;


    public virtual void Initialize() { }
    public virtual void Upgrade()
    {
        level++;
    }
}
