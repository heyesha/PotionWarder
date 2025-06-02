using UnityEngine;

public class OnCauldronTrigger : MonoBehaviour
{
    private Vector3 originalPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && other.gameObject.tag != "BottleIngredient")
        {
            other.GetComponent<MeshCollider>().enabled = false;
            Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                bool isStepCorrect = ingredient.Cauldron.CheckPlayerAction(ingredient.Name);
                Destroy(other.gameObject, 3f);
            }
        }
        else if (other.gameObject.tag == "BottleIngredient")
        {
            originalPosition = other.gameObject.GetComponent<MouseRotation>().originalPosition;

            var rotation = other.gameObject.GetComponent<MouseRotation>().originalRotation;
            other.transform.rotation = rotation;
            other.transform.position = originalPosition;

            other.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
}
