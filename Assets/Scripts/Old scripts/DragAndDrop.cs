using UnityEngine;

public class DragAndDrop3D : MonoBehaviour
{
    public float liftHeight = 2.0f; 
    public float liftSpeed = 2.0f;
    public float returnSpeed = 2.0f;

    private Camera mainCamera;
    private Transform currentObject;
    private Vector3 initialPosition;
    private GameObject selectedObject;
    private Vector3 originalPosition;
    private bool isDragging = false;

    [SerializeField]
    private Transform triggerArea;

    private bool isLifted = false;
    private bool isReturning = false;

    private Collider currentTrigger;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Draggable"))
            {
                selectedObject = hit.collider.gameObject;
                originalPosition = selectedObject.transform.position;
                isDragging = true;

                selectedObject.GetComponent<Collider>().enabled = false;
            }
        }

        if (isLifted)
        {
            LiftAndMoveObject();

            if (Input.GetMouseButtonUp(0))
            {
                isLifted = false;

                if (currentTrigger != null)
                {
                    StartCoroutine(RemoveObject());
                }
                else
                {
                    StartCoroutine(ReturnToInitialPosition());
                }
            }
        }
    }

    private void LiftAndMoveObject()
    {
        Vector3 currentPosition = selectedObject.transform.position;
        float newY = Mathf.Lerp(currentPosition.y, liftHeight, Time.deltaTime * liftSpeed);
        selectedObject.transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane horizontalPlane = new Plane(Vector3.up, Vector3.up * liftHeight);

        float distanceToPlane;
        if (horizontalPlane.Raycast(ray, out distanceToPlane))
        {
            Vector3 pointOnPlane = ray.GetPoint(distanceToPlane);;

            currentObject.position = new Vector3(pointOnPlane.x, newY, pointOnPlane.z);
        }
    }

    private System.Collections.IEnumerator RemoveObject()
    {
        float fallTime = 0.5f;
        Vector3 startPosition = currentObject.position;
        Vector3 endPosition = currentObject.position + Vector3.down * 2.0f;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / fallTime;
            currentObject.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        Destroy(currentObject.gameObject);
    }

    private System.Collections.IEnumerator ReturnToInitialPosition()
    {
        isReturning = true;
        Vector3 currentPosition = currentObject.position;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * returnSpeed;

            currentObject.position = Vector3.Lerp(currentPosition, initialPosition, t);
            yield return null;
        }

        isReturning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentTrigger = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentTrigger == other)
        {
            currentTrigger = null;
        }
    }
}
