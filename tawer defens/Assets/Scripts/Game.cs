using System;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [Header("Player Stats")]
    [SerializeField] private int startingMoney = 200;
    [SerializeField] private int startingLives = 20;

    public int Money { get; private set; }
    public int Lives { get; private set; }

    public event Action<int> OnMoneyChanged;
    public event Action<int> OnLivesChanged;
    public event Action<bool> OnGameEnded;
    public event Action<int> OnTowerBuilt;
    public event Action<int> OnEnemyKilled;

    public UnityEvent<int> MoneyEvent;
    public UnityEvent<int> LivesEvent;
    //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Money = startingMoney;
        Lives = startingLives;

        OnMoneyChanged?.Invoke(Money);
        OnLivesChanged?.Invoke(Lives);
        MoneyEvent?.Invoke(Money);
        LivesEvent?.Invoke(Lives);
    }

    // Resource System
    public bool HasResources(int amount) => Money >= amount;

    public void AddResources(int amount)
    {
        Money += amount;
        OnMoneyChanged?.Invoke(Money);
        MoneyEvent?.Invoke(Money);
    }

    public void SpendResources(int amount)
    {
        Money -= amount;
        OnMoneyChanged?.Invoke(Money);
        MoneyEvent?.Invoke(Money);
    }

    // Life / End
    public void LoseLife(int count = 1)
    {
        Lives -= count;
        OnLivesChanged?.Invoke(Lives);
        LivesEvent?.Invoke(Lives);

        if (Lives <= 0)
            EndGame(false);
    }

    public void EndGame(bool win)
    {
        Debug.Log(win ? "Victory" : "Defeat");
        OnGameEnded?.Invoke(win);
        Time.timeScale = 0;
    }

    // Global Event Triggers
    public void NotifyTowerBuilt(int id)
    {
        OnTowerBuilt?.Invoke(id);
    }

    public void NotifyEnemyKilled(int reward)
    {
        AddResources(reward);
        OnEnemyKilled?.Invoke(reward);
    }
}