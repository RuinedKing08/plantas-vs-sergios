using System;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [Header("Player Stats")]
    [SerializeField] private int startingMoney = 200;
    [SerializeField] private int startingLives = 20;

    public UnityEvent<int> OnMoneyChanged;
    public UnityEvent<int> OnLivesChanged;

    public int money;
    public int lives;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        money = startingMoney;
        lives = startingLives;
    }

    public bool HasResources(int amount) => money >= amount;

    public void AddResources(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void SpendResources(int amount)
    {
        money -= amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void LoseLife(int count = 1)
    {
        lives -= count;
        OnLivesChanged?.Invoke(lives);
        if (lives <= 0) EndGame(false);
    }

    public void EndGame(bool win)
    {
        Debug.Log(win ? "Victory!" : "Defeat!");
        Time.timeScale = 0;
    }
}