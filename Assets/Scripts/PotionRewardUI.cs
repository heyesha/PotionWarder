using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionRewardUI : MonoBehaviour
{
    [Header("Элементы")]
    [SerializeField] private string defaultRewardMessage = "Вы заработали ";
    [SerializeField] private GameObject closeButtonPrefab;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private TMP_Text clientMessageText;
    [SerializeField] private TMP_Text moneyRewardText;
    [SerializeField] private TMP_Text title;

    [Header("Сообщения")]
    [SerializeField] private string wrongPotionMessage = "Допущено слишком много ошибок, зелье опасно! Клиент отказался платить";
    [SerializeField] private string almostWrongPotionMessage = "Есть ошибки, зелье работает с побочными эффектами";
    [SerializeField] private string middlePotionMessage = "Среднее качество зелья. Побочных эффектов почти нет";
    [SerializeField] private string almostCorrectPotionMessage = "Зелье хорошего качества. Клиент остался доволен";
    [SerializeField] private string correctPotionMessage = "Зелье сварено идеально! Двойная оплата";

    private void Start()
    {
        var closeButton = closeButtonPrefab.GetComponent<Button>();
        closeButton.onClick.AddListener(HideReward);
        rewardPanel.SetActive(false);
    }

    public void ShowReward(float multiplier, int money)
    {
        moneyRewardText.text = defaultRewardMessage + $"{money} монет";

        if (multiplier <= 0f)
        {
            clientMessageText.text = wrongPotionMessage;
            CreateFailedTitle();
        }
        else if (multiplier > 0f && multiplier < 1f)
        {
            clientMessageText.text = almostWrongPotionMessage;
            CreateFailedTitle();
        }
        else if (multiplier >= 1f && multiplier <= 1.5f)
        {
            clientMessageText.text = middlePotionMessage;
            CreateMiddleTitle();
        }
        else if (multiplier > 1.5f && multiplier < 2)
        {
            clientMessageText.text = almostCorrectPotionMessage;
            CreateCorrectTitle();
        }
        else
        {
            clientMessageText.text = correctPotionMessage;
            CreateCorrectTitle();
        }
        rewardPanel.SetActive(true);
    }

    private void CreateCorrectTitle()
    {
        title.text = "Удачно!";
        if (ColorUtility.TryParseHtmlString("#35CF41", out Color greenColor))
        {
            title.color = greenColor;
        }
    }

    private void CreateFailedTitle()
    {
        title.text = "Неудачно!";
        if (ColorUtility.TryParseHtmlString("#CF3D35", out Color redColor))
        {
            title.color = redColor;
        }
    }

    private void CreateMiddleTitle()
    {
        title.text = "Средне!";
        if (ColorUtility.TryParseHtmlString("#CFC535", out Color yellowColor))
        {
            title.color = yellowColor;
        }
    }

    public void HideReward()
    {
        rewardPanel.SetActive(false);
    }
}
