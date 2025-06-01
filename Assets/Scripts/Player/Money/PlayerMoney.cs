using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    [SerializeField] private int _currentMoney = 0;

    public UnityEvent<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        OnMoneyChanged?.Invoke(_currentMoney);
    }

    public bool SpenMoney(int amount)
    {
        if (_currentMoney >= amount)
        {
            _currentMoney -= amount;
            OnMoneyChanged?.Invoke(_currentMoney);
            return true;
        }
        return false;
    }

    public int GetCurrentMoney()
    {
        return _currentMoney;
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney", _currentMoney);
    }

    public void LoadMoney()
    {
        _currentMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        OnMoneyChanged?.Invoke(_currentMoney);
    }
}
