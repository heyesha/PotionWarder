using TMPro;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] private string objectName = string.Empty;
    [SerializeField] private TMP_Text tooltipUIText;
    [SerializeField] private Vector3 offset = new Vector3(20, -20, 0);

    private void Start()
    {
        if (tooltipUIText == null)
        {
            tooltipUIText = GameObject.Find("TooltipText").GetComponent<TMP_Text>();
        }
    }
    private void OnMouseOver()
    {
        if (tooltipUIText != null)
        {
            tooltipUIText.text = objectName;
            tooltipUIText.transform.position = Input.mousePosition + offset;
            tooltipUIText.gameObject.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (tooltipUIText != null)
        {
            tooltipUIText.text = string.Empty;
            tooltipUIText.gameObject.SetActive(false);
        }
    }
}
