using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text timerText;

    private void OnEnable()
    {
        if (Game.Instance == null) return;
        Game.Instance.OnMoneyChanged += UpdateMoney;
        Game.Instance.OnLivesChanged += UpdateLives;

        UpdateMoney(Game.Instance.Money);
        UpdateLives(Game.Instance.Lives);
        Timer.Instance.OnSecondTick += UpdateTimer;
    }

    private void Start()
    {
        if (Game.Instance == null) return;

        Game.Instance.OnMoneyChanged += UpdateMoney;
        Game.Instance.OnLivesChanged += UpdateLives;

        UpdateMoney(Game.Instance.Money);
        UpdateLives(Game.Instance.Lives);
    }

    private void OnDisable()
    {
        Game.Instance.OnMoneyChanged -= UpdateMoney;
        Game.Instance.OnLivesChanged -= UpdateLives;
        Timer.Instance.OnSecondTick -= UpdateTimer;
    }


    private void UpdateTimer(float time)
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"time: {minutes:00}:{seconds:00}";
    }

    private void UpdateMoney(int value) => moneyText.text = $"money: ${value}";
    private void UpdateLives(int value) => livesText.text = $"lives: {value}";
}
