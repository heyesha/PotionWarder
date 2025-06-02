using TMPro;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    private float height;
    private Vector3 originalPosition;
    [SerializeField] private GameObject ingredientPrefab;
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
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
