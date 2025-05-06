using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDragAndDrop : MonoBehaviour
{
    [SerializeField, Tooltip("Скорость перемещения")] private float speed = 2;
    [SerializeField] private float height = 2.0f;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = new Vector3(Input.GetAxis("Mouse X") * speed * Time.deltaTime, 0, Input.GetAxis("Mouse Y") * speed * Time.deltaTime);

            transform.Translate(mouse * speed);
        }
    }
}
