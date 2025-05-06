using UnityEngine;

public class PlaceChecker : MonoBehaviour
{
    [SerializeField]
    private float height;

    private void Update()
    {
        var position = transform.position;
        position.y = height;
        transform.position = position;
    }
}
