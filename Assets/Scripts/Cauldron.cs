using System;
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

    public List<GameObject> Ingredients;
    public float waterAmount;
    public int TotalPoints;
    public int PerfectPoints;

    [SerializeField]
    private UnityEvent<string> OnTemperatureChanged;
    [SerializeField]
    private UnityEvent<float> OnTemperatureChangedPercent;

    private bool isHeating = false;


    [SerializeField]
    private int pointsForIngredient = 30;

    [SerializeField]
    private int wrongIngredient = 30;

    public void AddIngredient(GameObject ingredient)
    {
        Ingredients.Add(ingredient);
    }

    public void AddWater(int amount)
    {
        waterAmount += amount;
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
        OnTemperatureChanged?.Invoke($"{(int)temperature}��");
    }

    private void OnMouseUp()
    {
        isHeating = false;
    }

    [SerializeField]
    public UnityEvent<string> OnCheckedIngredients;

    public void CheckIngredients(PotionData potion)
    {
        var points = pointsForIngredient * Ingredients.Count;
        var perfectPoints = points;

        if (Ingredients.Count == 0)
        {
            OnCheckedIngredients?.Invoke("����� �������� �����������");
            return;
        }

        // �������� ������������ ������������
        if (Ingredients.Count == potion.recipe.Length)
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                if (Ingredients[i].GetComponent<Ingredient>().Name != potion.recipe[i].name)
                {
                    points -= wrongIngredient;
                }
            }
        }
        else
        {
            var diff = Math.Abs(potion.recipe.Length - Ingredients.Count);
            points -= diff * pointsForIngredient;
            Debug.Log("������������ ���������� ������������");
            return;
        }

        // TO DO �������� �� ���������� ���� � �����������

        if (points <= 0)
        {
            Debug.Log("����� ������������ �����������. ������ ����, �� ��������");
        }
        else if (points / perfectPoints < 0.3)
        {
            Debug.Log("����� ������������ �����. ������ �������� ����� ���������");
        }
        else if (points / perfectPoints < 0.6)
        {
            Debug.Log("����� ������������ ������. ������ �������� �������� ���������");
        }
        else if (points / perfectPoints == 1.0)
        {
            Debug.Log("����� ������������ ��������. ������ �������");
        }

        TotalPoints = points;
        PerfectPoints = perfectPoints;
    }
}