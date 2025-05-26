using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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

    private bool isHeating = false;

    public PotionData currentRecipe;
    public int currentStepIndex = 0;
    public List<RecipeStep> completedSteps = new List<RecipeStep>();
    public int wrongStepCount;
    public int correctStepCount;
    public bool isCurrentStepCorrect = false;

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
            // ЛОГИКА СПАВНА СЛАЙДЕРА

            isStepCorrect = true;
        }

        if (isStepCorrect)
        {
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
            wrongStepCount++;
            Debug.Log("Ошибка! Это не тот шаг.");
            currentStepIndex++;
            // TO DO: убрать инкремент шага, чтобы просто считать количество ошибок на шаге, если их больше 3,
            // то шаг переходит на следующий и он становится красным
            return false;
        }
    }

    public void SetRecipe(PotionData recipe)
    {
        currentRecipe = recipe;
    }

    public void ResetRecipe()
    {
        currentStepIndex = 0;
        completedSteps.Clear();
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

    [SerializeField]
    public UnityEvent<string> OnCheckedIngredients;

/*    public void CheckIngredients(PotionData potion)
    {
        var points = pointsForIngredient * Ingredients.Count;
        var perfectPoints = points;

        if (Ingredients.Count == 0)
        {
            OnCheckedIngredients?.Invoke("Нужно добавить ингредиенты");
            return;
        }

        // Проверка правильности ингредиентов
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
            Debug.Log("Неправильное количество ингредиентов");
            return;
        }

        // TO DO Проверка на количество ВОДЫ и ТЕМПЕРАТУРЫ

        if (points <= 0)
        {
            Debug.Log("Зелье приготовлено неправильно. Клиент ушёл, не заплатив");
        }
        else if (points / perfectPoints < 0.3)
        {
            Debug.Log("Зелье приготовлено плохо. Клиент заплатил треть стоимости");
        }
        else if (points / perfectPoints < 0.6)
        {
            Debug.Log("Зелье приготовлено хорошо. Клиент заплатил половину стоимости");
        }
        else if (points / perfectPoints == 1.0)
        {
            Debug.Log("Зелье приготовлено идеально. Клиент доволен");
        }

        TotalPoints = points;
        PerfectPoints = perfectPoints;
    }
*/}