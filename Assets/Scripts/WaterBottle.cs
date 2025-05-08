using UnityEngine;
using UnityEngine.Events;

public class WaterBottle : MonoBehaviour
{
    [SerializeField]
    private float waterAddingSpeed = 2;

    [SerializeField]
    private GameObject Cauldron;

    private void Start()
    {
        Cauldron = GameObject.FindGameObjectWithTag("Cauldron");
    }

    private void OnTriggerStay(Collider other)
    {
        var cauldronScript = Cauldron.GetComponent<Cauldron>();
        cauldronScript.waterAmount += waterAddingSpeed * Time.deltaTime;
    }
}
