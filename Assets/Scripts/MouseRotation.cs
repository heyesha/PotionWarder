using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    private Vector3 pointScreen;
    private float height;

    [SerializeField, Tooltip("Скорость перемещения")] 
    private float speed = 2.0f;

    [SerializeField]
    private float rotationSpeed = 8.0f;

    [SerializeField]
    private float radius = 2f;

    [SerializeField]
    public GameObject placeChecker;

    private GameObject Cauldron;
    private Quaternion targetRotation;
    private bool isNotOriginalPosition;
    private bool isInTrigger;

    //public GameObject selectedObject;
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    private void Start()
    {
        placeChecker = GameObject.FindGameObjectWithTag("PlaceChecker");
        Cauldron = GameObject.FindGameObjectWithTag("Cauldron");
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        height = originalPosition.y + 0.25f;
        if (tag == "Stick")
        {  
            height = originalPosition.y += 0.1f;
            originalPosition = Cauldron.transform.position;
        }
    }

    private void OnMouseDown()
    {
        Vector3 rotate = transform.eulerAngles;
        if (tag == "Stick")
        {
            rotate.x = -90; rotate.z = 0;
        }
        else
        {
            var trigger = GameObject.FindWithTag("RotateBottleTrigger");
            trigger.GetComponent<Collider>().enabled = true;
            rotate.x = 0; rotate.z = 0;
        }
        transform.rotation = Quaternion.Euler(rotate);

        this.GetComponent<Rigidbody>().freezeRotation = true;
        
        pointScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointScreen.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        Vector3 newPosition = Vector3.Lerp(transform.position, curPosition, speed * Time.deltaTime);

        newPosition.y = height;
        newPosition.x = Mathf.Clamp(newPosition.x, originalPosition.x - radius, originalPosition.x + radius);
        newPosition.z = Mathf.Clamp(newPosition.z, originalPosition.z - radius, originalPosition.z + radius);

        transform.position = newPosition;

        if (!isInTrigger && tag == "BottleIngredient")
        {
            targetRotation = originalRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 10f)
            {
                isNotOriginalPosition = false;
            }
        }
    }

    private void OnMouseUp()
    {
        var trigger = GameObject.FindWithTag("RotateBottleTrigger");
        trigger.GetComponent<Collider>().enabled = false;
        this.GetComponent<Rigidbody>().freezeRotation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RotateBottleTrigger") && tag == "BottleIngredient")
        {
            targetRotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x + 120f,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
            isInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RotateBottleTrigger") && tag == "BottleIngredient")
        {
            if (targetRotation != originalRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                targetRotation = originalRotation;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RotateBottleTrigger") && tag == "BottleIngredient")
        {
            isNotOriginalPosition = true;
            isInTrigger = false;
        }
    }
}
