using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionRewardUI : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private string defaultRewardMessage = "�� ���������� ";
    [SerializeField] private GameObject closeButtonPrefab;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private TMP_Text clientMessageText;
    [SerializeField] private TMP_Text moneyRewardText;
    [SerializeField] private TMP_Text title;

    [Header("���������")]
    [SerializeField] private string wrongPotionMessage = "�������� ������� ����� ������, ����� ������! ������ ��������� �������";
    [SerializeField] private string almostWrongPotionMessage = "���� ������, ����� �������� � ��������� ���������";
    [SerializeField] private string middlePotionMessage = "������� �������� �����. �������� �������� ����� ���";
    [SerializeField] private string almostCorrectPotionMessage = "����� �������� ��������. ������ ������� �������";
    [SerializeField] private string correctPotionMessage = "����� ������� ��������! ������� ������";

    private void Start()
    {
        var closeButton = closeButtonPrefab.GetComponent<Button>();
        closeButton.onClick.AddListener(HideReward);
        rewardPanel.SetActive(false);
    }

    public void ShowReward(float multiplier, int money)
    {
        moneyRewardText.text = defaultRewardMessage + $"{money} �����";

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
        title.text = "������!";
        if (ColorUtility.TryParseHtmlString("#35CF41", out Color greenColor))
        {
            title.color = greenColor;
        }
    }

    private void CreateFailedTitle()
    {
        title.text = "��������!";
        if (ColorUtility.TryParseHtmlString("#CF3D35", out Color redColor))
        {
            title.color = redColor;
        }
    }

    private void CreateMiddleTitle()
    {
        title.text = "������!";
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
