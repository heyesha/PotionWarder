using UnityEngine;
using System.Text;
using TMPro;
using System;

public class RecipeUI : MonoBehaviour
{
    public Cauldron cauldron;
    public TMP_Text stepsText;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        cauldron = GameObject.FindGameObjectWithTag("Cauldron").GetComponent<Cauldron>();
        stepsText = GetComponent<TMP_Text>();
        cauldron.OnCheckAction.AddListener(UpdateUI);
        cauldron.OnCreateOrder.AddListener(WritePotionStepsText);
    }

    public void WritePotionStepsText()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var step in cauldron.currentRecipe.steps)
        {
            sb.AppendLine($"<color=yellow>→ {step.description}</color>");
        }
        stepsText.text = sb.ToString();
    }

    public void UpdateUI()
    {
        if (cauldron == null && cauldron.currentRecipe == null)
        {
            Debug.LogError("Котла или рецепта не существует!");
            return;
        }

        StringBuilder sb = new StringBuilder();

        var checkedSteps = cauldron.checkedSteps;
        int lastCheckedStep = 0;
        for (int i = 0; i < checkedSteps.Count; i++)
        {
            if (!checkedSteps.ContainsKey(i))
            {
                Debug.LogError("Шага с таким ID не найдено!");
                return;
            }

            string stepDesc = cauldron.currentRecipe.steps[i].description;

            if (checkedSteps[i])
            {
                sb.AppendLine($"<color=green>→ {stepDesc}</color>");
            }
            else if (!checkedSteps[i])
            {
                sb.AppendLine($"<color=red>→ {stepDesc}</color>");
            }
            /*else
            {
                sb.AppendLine($"<color=yellow>{stepDesc}</color>");
            }*/
            lastCheckedStep = i + 1;
        }

        var unCheckedSteps = cauldron.currentRecipe.steps;

        if (lastCheckedStep >= unCheckedSteps.Count)
        {
            stepsText.text = sb.ToString();
            return;
        }

        if (lastCheckedStep != unCheckedSteps.Count)
        {
            for (int i = lastCheckedStep; i < unCheckedSteps.Count; i++)
            {
                string stepDesc = unCheckedSteps[i].description;
                sb.AppendLine($"<color=yellow>→ {stepDesc}</color>");
            }
        }
        
        stepsText.text = sb.ToString();
    }

    /*private void Update()
    {
        if (cauldron.currentRecipe != null)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cauldron.currentRecipe.checkedSteps.Count; i++)
            {
                string stepDesc = cauldron.currentRecipe.checkedSteps[i].description;

                if (i < cauldron.currentStepIndex && cauldron.isCurrentStepCorrect)
                {
                    sb.AppendLine($"<color=green>✓ {stepDesc}</color>");
                }
                else if (i == cauldron.currentStepIndex)
                {
                    sb.AppendLine($"<color=yellow>→ {stepDesc}</color>");
                }
                else if (!cauldron.isCurrentStepCorrect)
                {
                    sb.AppendLine($"<color=red>→ {stepDesc}</color>");
                }

                else
                {
                    sb.AppendLine($"- {stepDesc}");
                }
            }
            stepsText.text = sb.ToString();
        }
    }*/
}