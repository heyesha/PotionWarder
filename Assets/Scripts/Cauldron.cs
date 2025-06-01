using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private float temperature = 20f;

    [SerializeField] private float heatingSpeed = 8f;

    [SerializeField] private float maxTemperature = 120f;

    [SerializeField] private float minTemperature = 20f;

    [SerializeField] private float coolingSpeed = 2f;

    public float maxWaterAmount = 100;

    public List<GameObject> Ingredients;

    public float WaterAmount = 19;

    public UnityEvent<float> OnWaterAmountChanged;

    public UnityEvent<string> OnWaterAmountChangedString;

    [SerializeField]
    private UnityEvent<string> OnTemperatureChanged;
    [SerializeField]
    private UnityEvent<float> OnTemperatureChangedPercent;

    public UnityEvent OnCheckAction;
    public UnityEvent OnCreateOrder;
    public UnityEvent<string> OnPotionReady;

    private bool isHeating = false;

    public PotionData currentRecipe;
    public int currentStepIndex = 0;
    public List<RecipeStep> completedSteps = new List<RecipeStep>();
    public int wrongStepCount;
    public int correctStepCount;
    public bool isCurrentStepCorrect = false;
    public Dictionary<int, bool> checkedSteps = new Dictionary<int, bool>();

    private void Start()
    {
        OnWaterAmountChangedString?.Invoke($"{WaterAmount}л");
    }

    public bool CheckPlayerAction(string addedIngredientName = null, 
        int addedWater = 0, bool isStirred = false)
    {
        if (currentRecipe == null)
        {
            Debug.Log("Нет заказа!");
            return false;
        }

        if (currentStepIndex >= currentRecipe.steps.Count)
        {
            Debug.Log("Рецепт уже завершён!");
            return false;
        }

        RecipeStep currentStep = currentRecipe.steps[currentStepIndex];

        bool isStepCorrect = false;

        if (!string.IsNullOrEmpty(currentStep.requiredIngredient))
        {
            if (currentStep.requiredTemperature != 0)
            {
                var diff = Math.Abs(currentStep.requiredTemperature - temperature);
                if (addedIngredientName == currentStep.requiredIngredient 
                    && diff <= currentStep.allowableDifference)
                {
                    isStepCorrect = true;
                }
            }

            else
            {
                if (addedIngredientName == currentStep.requiredIngredient)
                {
                    isStepCorrect = true;
                }
            }
        }

        else if (currentStep.requiredWaterAmount > 0)
        {
            if (addedWater == currentStep.requiredWaterAmount)
            {
                isStepCorrect = true;
            }
        }

        else if (currentStep.isNeedToStir && isStirred)
        {
            isStepCorrect = true;
        }

        if (isStepCorrect)
        {
            AddCheckedStep(currentStepIndex, isStepCorrect);
            isCurrentStepCorrect = true;
            correctStepCount++;
            completedSteps.Add(currentStep);
            currentStepIndex++;
            Debug.Log($"Шаг {currentStepIndex} выполнен: {currentStep.description}");

            if (currentStepIndex == currentRecipe.steps.Count)
            {
                Debug.Log("Рецепт выполнен идеально!");
            }
            return true;
        }
        else
        {
            AddCheckedStep(currentStepIndex, isStepCorrect);
            wrongStepCount++;
            Debug.Log("Ошибка! Это не тот шаг.");
            currentStepIndex++;
            // TO DO: убрать инкремент шага, чтобы просто считать количество ошибок на шаге, если их больше 3,
            // то шаг переходит на следующий и он становится красным
            return false;
        }
    }

    public void CheckPotion()
    {
        if (currentRecipe == null)
        {
            return;
        }

        if (currentStepIndex == currentRecipe.steps.Count)
        {
            CalculatePotionPrice(currentRecipe.basicPrice);
        }
        else
        {
            Debug.Log("ЗЕЛЬЕ НЕ ЗАКОНЧЕНО!");
        }

        ResetRecipe();
    }

    public void CalculatePotionPrice(int defaultPrice)
    {
        var potionChecker = GetComponent<PotionChecker>();
        var multiplyier = potionChecker.CalculateQualityMultiplier(wrongStepCount, correctStepCount);

        var price = (int)(defaultPrice * multiplyier);
        UpdateMoney(price);

        OnPotionReady?.Invoke(price.ToString());
    }

    private void UpdateMoney(int amount)
    {
        PlayerMoney.Instance.AddMoney(amount);
    }

    private void AddCheckedStep(int stepIndex, bool isCorrect)
    {
        checkedSteps.Add(stepIndex, isCorrect);
        OnCheckAction?.Invoke();
    }

    public void SetRecipe(PotionData recipe)
    {
        currentRecipe = recipe;
        OnCreateOrder?.Invoke();
    }

    public void ResetRecipe()
    {
        wrongStepCount = 0;
        correctStepCount = 0;
        currentStepIndex = 0;
        completedSteps.Clear();
        checkedSteps.Clear();
    }

    public void AddWater(float waterAddingSpeed)
    {
        if (WaterAmount < maxWaterAmount)
        {
            WaterAmount += waterAddingSpeed * Time.deltaTime;
            OnWaterAmountChanged?.Invoke((float)Math.Floor(WaterAmount));
        }
        else
        {
            Debug.Log("В котле максимальное количество воды!");
            // TODO: спавн уведомления на экране UI
        }
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
        OnTemperatureChanged?.Invoke($"{(int)temperature}°С");
    }

    private void OnMouseUp()
    {
        isHeating = false;
    }
}