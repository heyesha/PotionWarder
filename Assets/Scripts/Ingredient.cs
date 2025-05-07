using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;
    public int Amount;
    public GameObject Cauldron;

    private void Start()
    {
        Cauldron = GameObject.FindGameObjectWithTag("Cauldron");
    }
}
