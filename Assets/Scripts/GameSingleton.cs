using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSingleton : MonoBehaviour
{
    public SoundManager Sounds;
    public levelManager levelManager;
    public UIManager UI;
    public Inventory Inventory;
    [SerializeField] Text MoneyText;

    private static GameSingleton _instance;
    public static GameSingleton Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


   
}
