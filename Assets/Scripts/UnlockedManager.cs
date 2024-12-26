using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedManager : MonoBehaviour
{
    public int ID;
    public string Name;
    [SerializeField] AudioSource audio;
    [SerializeField] List<GameObject> activatedGameObject;
    [SerializeField] List<GameObject> deactivatedGameObject;
    [SerializeField] Image MoneyFiller;
    [SerializeField] TextMeshProUGUI MoneyText;
    public float TotalMoney;
    public float investedPrice;
    public bool isUnlocked = false;


    void Awake()
    {
        refreshMoney();
    }


    void refreshMoney()
    {
        MoneyFiller.fillAmount = investedPrice / TotalMoney;
        MoneyText.text = (TotalMoney - investedPrice).ToString();
    }

    void unlock()
    {
         GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.OpenStation);
        isUnlocked = true;
        foreach (GameObject go in activatedGameObject)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in deactivatedGameObject)
        {
            go.SetActive(false);
        }
    }

    public int Payment(int givenPrice)
    {
        if(TotalMoney - investedPrice > 0)
        {
            investedPrice += givenPrice;
            refreshMoney();
            return -givenPrice;
        }
        else
        {
            unlock();
            return -(int)(TotalMoney - investedPrice);
        }
    }
}
