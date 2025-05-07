using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomerSpawner : MonoBehaviour
{
    public PotionData[] allPotions;
    public GameObject customerPrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, 0);

    [SerializeField]
    private UnityEvent<string> OnOrderText;

    private void Start()
    {
        allPotions = GameManager.Instance.allPotions;
        
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);

        Customer customerScript = customer.GetComponent<Customer>();

        PotionData randomPotion = allPotions[Random.Range(0, allPotions.Length)];
        customerScript.SetOrder(randomPotion);

        GameManager.Instance.CurrentOrder = randomPotion;
        // TO DO: награда

        OnOrderText?.Invoke($"Заказано зелье: {randomPotion.potionName}");
    }
}

