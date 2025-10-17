using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text livesText;

    private void OnEnable()
    {
        if (Game.Instance == null) return;
        Game.Instance.OnMoneyChanged += UpdateMoney;
        Game.Instance.OnLivesChanged += UpdateLives;

        UpdateMoney(Game.Instance.Money);
        UpdateLives(Game.Instance.Lives);
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
    }

    private void UpdateMoney(int value) => moneyText.text = $"money: ${value}";
    private void UpdateLives(int value) => livesText.text = $"lives: {value}";
}
