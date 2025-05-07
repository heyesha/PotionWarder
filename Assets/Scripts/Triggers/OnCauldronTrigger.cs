using UnityEngine;
using UnityEngine.Events;

public class OnCauldronTrigger : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<GameObject> OnAddIngredient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            OnAddIngredient?.Invoke(other.gameObject);
        }
    }
}
