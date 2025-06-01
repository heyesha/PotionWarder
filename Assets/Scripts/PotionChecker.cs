using UnityEngine;

public class PotionChecker : MonoBehaviour
{
    [SerializeField] private int perfectStepOrderReward = 2;

    [SerializeField] private float bonus = 1f;

    public float CalculateQualityMultiplier(int wrongStepsCount, int correctStepsCount)
    {
        float qualityMultiplier;

        if (correctStepsCount <= 0)
        {
            return 0f;
        }

        var stepsCount = wrongStepsCount + correctStepsCount;
        var correctStepsPercent = (float)correctStepsCount / (float)stepsCount;
        qualityMultiplier = correctStepsPercent;

        if (correctStepsCount == stepsCount) // ÈÄÅÀËÜÍÎÅ ÇÅËÜÅ
        {
            qualityMultiplier = correctStepsPercent * perfectStepOrderReward;
        }
        else if (correctStepsCount >= wrongStepsCount)
        {
            qualityMultiplier += bonus;
        }

        return qualityMultiplier;
    }

    public void PayMoney()
    {

    }
}
