using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PotionData[] allPotions;
    public PotionData CurrentOrder;
    public GameObject Cauldron;

    public UnityEvent<string> OnCreateOrderText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            allPotions = Resources.LoadAll<PotionData>("");
            Cauldron = GameObject.FindGameObjectWithTag("Cauldron");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateOrder()
    {
        PotionData randomPotion = allPotions[Random.Range(0, allPotions.Length)];

        Instance.CurrentOrder = randomPotion;
        OnCreateOrderText?.Invoke($"Заказ:\n{randomPotion.potionName}");
        if (Cauldron != null)
        {
            var cauldron = Cauldron.GetComponent<Cauldron>();
            cauldron.ResetRecipe();
            cauldron.SetRecipe(CurrentOrder);
        }
    }

    public void CheckPotion()
    {
        
    }
}
