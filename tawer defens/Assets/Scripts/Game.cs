using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }


    [SerializeField] private EventHub events;
    [SerializeField] private Money money;
    public EventHub Events => events;
    public Money Money => money;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}


public sealed class EventHub : MonoBehaviour
{
    public event Action OnEnemyKilled;
    public event Action OnTowerBuilt;
    public event Action OnBaseDestroyed;


    public Func<float> GetCostModifier;


    public void EnemyKilled() => OnEnemyKilled?.Invoke();
    public void TowerBuilt() => OnTowerBuilt?.Invoke();
    public void BaseDestroyed() => OnBaseDestroyed?.Invoke();
}
