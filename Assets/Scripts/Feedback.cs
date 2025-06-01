using TMPro;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    [SerializeField] private GameObject feedbackTextPrefab;
    [SerializeField] private Transform cauldronTransform;

    public void ShowFeedbackText(string message)
    {
        var go = Instantiate(feedbackTextPrefab, cauldronTransform.position + Vector3.up, Quaternion.identity);
        TextMeshPro textMesh = go.GetComponent<TextMeshPro>();
    }
}
