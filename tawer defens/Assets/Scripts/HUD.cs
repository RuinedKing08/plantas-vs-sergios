using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        if (Timer.Instance != null)
            Timer.Instance.OnSecondTick += UpdateTimer;
    }

    private void OnEnable()
    {
        if (Game.Instance != null)
        {
            Game.Instance.OnMoneyChanged += UpdateMoney;
            Game.Instance.OnLivesChanged += UpdateLives;
            UpdateMoney(Game.Instance.Money);
            UpdateLives(Game.Instance.Lives);
        }

        if (Timer.Instance != null)
            Timer.Instance.OnSecondTick += UpdateTimer;
    }

    private void OnDisable()
    {
        if (Game.Instance != null)
        {
            Game.Instance.OnMoneyChanged -= UpdateMoney;
            Game.Instance.OnLivesChanged -= UpdateLives;
        }

        if (Timer.Instance != null)
            Timer.Instance.OnSecondTick -= UpdateTimer;
    }

    private void UpdateTimer(float time)
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"time: {minutes:00}:{seconds:00}";
    }

    private void UpdateMoney(int value)
    {
        if (moneyText != null)
            moneyText.text = $"money: ${value}";
    }

    private void UpdateLives(int value)
    {
        if (livesText != null)
            livesText.text = $"lives: {value}";
    }
}
