using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Potions/Potion")]
public class PotionData : ScriptableObject
{
    public string potionName;
    public string description;
    public int waterAmount;
    public int temperature;
    public PotionIngredient[] recipe;
}

[System.Serializable]
public class PotionIngredient
{
    public string name;
    public int amount;
}