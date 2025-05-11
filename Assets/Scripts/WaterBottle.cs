using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class WaterBottle : MonoBehaviour
{
    [SerializeField]
    private float waterAddingSpeed = 1.5f;

    private int startWaterAmount;
    private int endWaterAmount;

    [SerializeField]
    private GameObject Cauldron;

    [SerializeField] private Cauldron cauldronScript;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Cauldron = GameObject.FindGameObjectWithTag("Cauldron");
        if (Cauldron != null)
        {
            cauldronScript = Cauldron.GetComponent<Cauldron>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        startWaterAmount = (int)Math.Floor(cauldronScript.WaterAmount);
    }

    private void OnTriggerStay(Collider other)
    { 
        //cauldronScript.WaterAmount += waterAddingSpeed * Time.deltaTime;
        //cauldronScript.AddWater(waterAddingSpeed);

        if (Cauldron != null)
        {
            if (cauldronScript.WaterAmount < cauldronScript.maxWaterAmount)
            {
                cauldronScript.WaterAmount += waterAddingSpeed * Time.deltaTime;
                cauldronScript.OnWaterAmountChanged?.Invoke((float)(Math.Floor(cauldronScript.WaterAmount) / 100));
                cauldronScript.OnWaterAmountChangedString?.Invoke(Convert.ToString((int)Math.Floor(cauldronScript.WaterAmount)) + "л");
            }
            else
            {
                Debug.Log("В котле максимальное количество воды!");
                // TODO: спавн уведомления на экране UI
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        endWaterAmount = (int)Math.Floor(cauldronScript.WaterAmount);
        var waterAdded = endWaterAmount - startWaterAmount;

        cauldronScript.CheckPlayerAction(null, waterAdded);
    }
}
