using UnityEngine;
using System.Text;
using TMPro;

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
    }

    private void Update()
    {
        if (cauldron.currentRecipe != null)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cauldron.currentRecipe.steps.Count; i++)
            {
                string stepDesc = cauldron.currentRecipe.steps[i].description;

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
    }
}