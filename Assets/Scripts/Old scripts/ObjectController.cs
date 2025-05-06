using System.Collections;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedObject;
    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool isReturning = false;

    [SerializeField]
    private Transform triggerArea;
    [SerializeField]
    private float returnSpeed = 1.0f;

    // Максимальная и минимальная высота для движения объекта
    [SerializeField]
    private float minHeight = 2.3f;
    [SerializeField]
    private float maxHeight = 2.3f;

    public Vector3 centerPoint = new Vector3(0, 0, 0);
    public float dragRadius = 5.0f;

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectObject();
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseObject();
        }
    }

    private void TrySelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Draggable"))
            {
                selectedObject = hit.collider.gameObject;
                originalPosition = selectedObject.transform.position;
                isDragging = true;

                // Отключаем коллайдер выбранного объекта
                selectedObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void DragObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // Ограничиваем высоту только по оси Y
            targetPosition.y = Mathf.Clamp(targetPosition.y, minHeight, maxHeight);

            selectedObject.transform.position = targetPosition;
        }
    }

    private void ReleaseObject()
    {
        isDragging = false;

        if (selectedObject != null)
        {
            // Включаем коллайдер обратно
            selectedObject.GetComponent<Collider>().enabled = true;
            Debug.Log("Коллайдер вкл");

            // Если объект отпущен не в зоне, возвращаем его на начальное место
            if (!IsInsideTriggerArea(selectedObject.transform.position))
            {
                StartCoroutine(MoveToOriginalPosition());
                //selectedObject.transform.position = originalPosition;
            }

            //selectedObject = null;
            Debug.Log("Обнуление");
        }
    }

    private IEnumerator MoveToOriginalPosition()
    {
        isReturning = true;
        float elapsedTime = 0;
        Vector3 startingPosition = selectedObject.transform.position;

        while (elapsedTime < 1.0f)
        {
            selectedObject.transform.position = Vector3.Lerp(startingPosition, originalPosition, elapsedTime);
            elapsedTime += Time.deltaTime * returnSpeed;
            yield return null;
        }

        //selectedObject.transform.position = originalPosition;

        if (selectedObject == gameObject)
        {
            selectedObject = null;
        }

        isReturning = false; // Сбрасываем флаг
    }

private bool IsInsideTriggerArea(Vector3 position)
    {
        if (triggerArea != null)
        {
            Collider triggerCollider = triggerArea.GetComponent<Collider>();
            return triggerCollider.bounds.Contains(position);
        }

        return false;
    }
}
