using System.Collections;
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

            //Debug.Log("До корутины");
            //StartCoroutine(MoveToOriginalPosition(other));
            //Debug.Log("После корутины");
        }
    }

    private IEnumerator MoveToOriginalPosition(Collider other)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = other.transform.position;

        while (elapsedTime < 1.0f)
        {
            other.transform.position = Vector3.Lerp(startingPosition, originalPosition, elapsedTime);
            elapsedTime += Time.deltaTime * returnSpeed;
            yield return null;
        }
    }
}
