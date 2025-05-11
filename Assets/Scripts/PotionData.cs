using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Potions/Potion")]
public class PotionData : ScriptableObject
{
    public string potionName;
    public List<RecipeStep> steps;
}

[System.Serializable]
public class PotionIngredient
{
    public string name;
    public int amount;
}

[System.Serializable]
public class RecipeStep
{
    public string description;
    public string requiredIngredient;
    public int requiredWaterAmount;
}