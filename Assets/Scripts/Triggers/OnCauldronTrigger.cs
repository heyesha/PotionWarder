using UnityEngine;

public class OnCauldronTrigger : MonoBehaviour
{
    private GameObject cauldron;
    private void Start()
    {
        cauldron = GameObject.FindWithTag("Cauldron");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.transform.SetParent(cauldron.transform);
            //other.enabled = false;
        }
    }
}
