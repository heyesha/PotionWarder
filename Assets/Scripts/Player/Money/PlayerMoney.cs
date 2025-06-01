using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    [SerializeField] private int _currentMoney;

    public int CurrentMoney
    {
        get {  return _currentMoney; }
    }

    public UnityEvent<int> OnMoneyChanged;

    private void Start()
    {
        LoadMoney();
        OnMoneyChanged?.Invoke(_currentMoney);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadMoney();
            OnMoneyChanged?.Invoke(_currentMoney);
        }
        else
        {
            Destroy(gameObject);
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

    private void OnApplicationQuit()
    {
        SaveMoney();
    }

    private void OnDestroy()
    {
        SaveMoney();
    }
}
