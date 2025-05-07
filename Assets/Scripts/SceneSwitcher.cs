using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwithcer : MonoBehaviour
{
    public void SwitchToCauldronScene()
    {
        SceneManager.LoadScene("CauldronScene");
    }

    public void SwitchToClientScene()
    {
        SceneManager.LoadScene("ClientScene");
    }
}
