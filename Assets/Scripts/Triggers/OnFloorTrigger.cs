using UnityEngine;

public class OnFloorTrigger : MonoBehaviour
{
    private Vector3 originalPosition;

    [SerializeField]
    private float returnSpeed = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            originalPosition = other.gameObject.GetComponent<MouseRotation>().originalPosition;

            var rotation = other.gameObject.GetComponent<MouseRotation>().originalRotation;
            other.transform.rotation = rotation;
            other.transform.position = originalPosition;

            other.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
}
