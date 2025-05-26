using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnCauldronTrigger : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<Ingredient> OnAddIngredient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {

            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                bool isStepCorrect = ingredient.Cauldron.CheckPlayerAction(ingredient.Name);

                if (isStepCorrect)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
