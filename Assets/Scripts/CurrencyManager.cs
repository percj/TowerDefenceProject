using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] Text CoinText;
    int Coin;
    // Start is called before the first frame update
    void Awake()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("TotalEarnedCoin", Coin);
        CoinText.text = Coin.ToString();
    }

    public void LoadData()
    {
        Coin =  PlayerPrefs.GetInt("TotalEarnedCoin", Coin);
        CoinText.text = Coin.ToString();
    }

    public void UseMoney(int usingMoney)
    {
        Coin-= usingMoney;
        SaveData();
    }

    public bool ControllMoney(int controlMoney)
    {
        return controlMoney <= Coin;
    }

    internal void AddMoney(int usingMoney)
    {
        Coin += usingMoney;
        SaveData();
    }
}
