using UnityEngine;

public class Customer : MonoBehaviour
{
    public PotionData currentOrder;

    public void SetOrder(PotionData potion)
    {
        currentOrder = potion;

        Debug.Log($"������ �������: {potion.name}");
    }

    public void ReceivePotion(PotionData potion)
    {
        if (potion == currentOrder)
        {
            Debug.Log("����� ����������. ������ �������!");
        }
        else
        {
            Debug.Log("������������ �����. ������ ���������");
        }
        Destroy(gameObject);
    }
}
