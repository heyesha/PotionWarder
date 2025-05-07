using UnityEngine;

public class Customer : MonoBehaviour
{
    public PotionData currentOrder;

    public void SetOrder(PotionData potion)
    {
        currentOrder = potion;

        Debug.Log($"Клиент заказал: {potion.name}");
    }

    public void ReceivePotion(PotionData potion)
    {
        if (potion == currentOrder)
        {
            Debug.Log("Зелье правильное. Клиент доволен!");
        }
        else
        {
            Debug.Log("Неправильное зелье. Клиент недоволен");
        }
        Destroy(gameObject);
    }
}
