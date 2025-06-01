using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    private void Start()
    {
        PlayerMoney.Instance.OnMoneyChanged.AddListener(UpdateMoneyUI);
        UpdateMoneyUI(PlayerMoney.Instance.CurrentMoney);
    }

    private void UpdateMoneyUI(int money)
    {
        _moneyText.text = money.ToString();
    }

    private void OnDestroy()
    {
        if (PlayerMoney.Instance != null)
        {
            PlayerMoney.Instance.OnMoneyChanged.RemoveListener(UpdateMoneyUI);
        }
    }
}
