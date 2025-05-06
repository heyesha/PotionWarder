using UnityEngine;
using UnityEngine.SceneManagement; // Для работы с переключением сцен

public class TableInteraction : MonoBehaviour
{
    private bool isPlayerNear = false; // Проверяем, рядом ли игрок
    private bool isPlayerInCauldronScene = false;

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, входит ли в зону объект с тэгом "Player"
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Игрок подошёл к столу. Нажмите E для перехода или Q для выхода.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Если игрок покидает зону триггера
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Игрок отошёл от стола.");
        }
    }

    void Update()
    {
        // Проверяем, рядом ли игрок и нажата ли кнопка взаимодействия
        if (isPlayerNear)
        {
            // При нажатии E — переход к новой сцене
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Смена сцены...");
                SceneManager.LoadScene("CauldronScene"); // Укажите название вашей новой сцены
                isPlayerInCauldronScene = true;
            }  
        }
        // При нажатии Q — возврат в основную сцену
        if (isPlayerInCauldronScene && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Возврат в основную сцену...");
            SceneManager.LoadScene("SampleScene"); // Укажите название вашей основной (инициализационной) сцены
            isPlayerInCauldronScene = false;
        }
    }
}
