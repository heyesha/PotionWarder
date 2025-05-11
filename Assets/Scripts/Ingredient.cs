using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;
    public Cauldron Cauldron;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Cauldron = GameManager.Instance.Cauldron.GetComponent<Cauldron>();
    }
}
