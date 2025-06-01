using UnityEngine;

public class OnCauldronTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.GetComponent<MeshCollider>().enabled = false;
            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                bool isStepCorrect = ingredient.Cauldron.CheckPlayerAction(ingredient.Name);
                Destroy(other.gameObject, 3f);
            }
        }
    }
}
