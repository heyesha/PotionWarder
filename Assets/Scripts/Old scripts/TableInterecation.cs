using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������ � ������������� ����

public class TableInteraction : MonoBehaviour
{
    private bool isPlayerNear = false; // ���������, ����� �� �����
    private bool isPlayerInCauldronScene = false;

    void OnTriggerEnter(Collider other)
    {
        // ���������, ������ �� � ���� ������ � ����� "Player"
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("����� ������� � �����. ������� E ��� �������� ��� Q ��� ������.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ���� ����� �������� ���� ��������
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("����� ������ �� �����.");
        }
    }

    void Update()
    {
        // ���������, ����� �� ����� � ������ �� ������ ��������������
        if (isPlayerNear)
        {
            // ��� ������� E � ������� � ����� �����
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("����� �����...");
                SceneManager.LoadScene("CauldronScene"); // ������� �������� ����� ����� �����
                isPlayerInCauldronScene = true;
            }  
        }
        // ��� ������� Q � ������� � �������� �����
        if (isPlayerInCauldronScene && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("������� � �������� �����...");
            SceneManager.LoadScene("SampleScene"); // ������� �������� ����� �������� (�����������������) �����
            isPlayerInCauldronScene = false;
        }
    }
}
