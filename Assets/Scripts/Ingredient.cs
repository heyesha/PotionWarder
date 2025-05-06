using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private float height;
    private Vector3 originalPosition;
    [SerializeField]
    private GameObject ingredientPrefab;
    private Outline outline;

    private void Start()
    {
        originalPosition = transform.position;
        height = originalPosition.y += 0.1f;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (GameObject.FindGameObjectsWithTag(ingredientPrefab.name).Length <= 3)
        {
            Instantiate(ingredientPrefab, originalPosition, Quaternion.identity);
        }
    }

    private void OnMouseOver()
    {
        // ÂÊËÞ×ÅÍÈÅ ÏÎÄÑÂÅÒÊÈ
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        // ÂÛÊËÞ×ÅÍÈÅ ÏÎÄÑÂÅÒÊÈ
        outline.enabled = false;
    }
}
