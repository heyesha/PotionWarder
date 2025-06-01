using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaterBottle : MonoBehaviour
{
    [SerializeField]
    private float waterAddingSpeed = 1.5f;

    private int startWaterAmount;
    private int endWaterAmount;

    private float addedWaterAmount = 0f;

    [SerializeField]
    private GameObject Cauldron;

    [SerializeField] private Cauldron cauldronScript;

    [SerializeField]
    private UnityEvent<string> OnAddWater;
    [SerializeField] private GameObject notificationPrefab;

    private TMP_Text notificationText;

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
            notificationText = Instantiate(notificationPrefab).GetComponent<TMP_Text>();
            notificationText.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        startWaterAmount = (int)Math.Floor(cauldronScript.WaterAmount);
        addedWaterAmount = 0f;
        notificationText.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    { 
        if (Cauldron != null)
        {
            if (cauldronScript.WaterAmount < cauldronScript.maxWaterAmount)
            {
                cauldronScript.WaterAmount += waterAddingSpeed * Time.deltaTime;
                addedWaterAmount += waterAddingSpeed * Time.deltaTime;
                if (notificationText != null)
                {
                    notificationText.text = "+" + Convert.ToString((int)Math.Floor(addedWaterAmount)) + "л";
                }
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
        cauldronScript.WaterAmount = (int)cauldronScript.WaterAmount;
        var waterAdded = endWaterAmount - startWaterAmount;
        addedWaterAmount = 0f;

        cauldronScript.CheckPlayerAction(null, waterAdded);

        if (notificationText != null)
        {
            notificationText.enabled = false;
        }
    }
}
