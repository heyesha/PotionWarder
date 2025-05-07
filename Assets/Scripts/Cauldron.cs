using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
    [SerializeField]
    private float temperature = 20f;
    [SerializeField]
    private float heatingSpeed = 8f;
    [SerializeField]
    private float maxTemperature = 120f;
    [SerializeField]
    private float minTemperature = 20f;
    [SerializeField]
    private float coolingSpeed = 2f;

    [SerializeField]
    private UnityEvent<string> OnTemperatureChanged;
    [SerializeField]
    private UnityEvent<float> OnTemperatureChangedPercent;

    //private List<GameObject> ingredients;
    private bool isHeating = false;

    public List<GameObject> Ingredients;

    public void AddIngredient(GameObject ingredient)
    {
        Ingredients.Add(ingredient);
    }

    private void OnMouseDrag()
    {
        isHeating = true;

        if (temperature <= maxTemperature)
        {
            temperature += heatingSpeed * Time.deltaTime;
            TemperatureChanged(temperature);
        }
    }
    private void FixedUpdate()
    {
        if (!isHeating && temperature >= minTemperature)
        {
            temperature -= coolingSpeed * Time.deltaTime;
            TemperatureChanged(temperature);
        }
    }

    void TemperatureChanged(float temperature)
    {
        OnTemperatureChangedPercent?.Invoke(temperature / maxTemperature);
        OnTemperatureChanged?.Invoke($"{(int)temperature}°Ñ");
    }

    private void OnMouseUp()
    {
        isHeating = false;
    }
}