using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySkin : MonoBehaviour
{
    public StoreItem BuyingItem;
    [SerializeField] Text buyItemPrice;
    [SerializeField] CurrencyManager currencyManager;
    [SerializeField] SoundManager SoundManager;
    [SerializeField] Image buttonColor;

    internal void UIRefresh()
    {
        buyItemPrice.text = BuyingItem.ItemPrice.ToString();
        if (currencyManager.ControllMoney(BuyingItem.ItemPrice))
        {
            GetComponent<Button>().interactable = true;
            buttonColor.color = Color.white;
        }
        else
        {
            GetComponent<Button>().interactable = false;
            buttonColor.color = Color.grey;
        }
    }

    public void Buy()
    {
        currencyManager.UseMoney(BuyingItem.ItemPrice);
        BuyingItem.BuyThisItem();
        BuyingItem.EquipItem();
        SoundManager.PlayOneShot(SoundManager.Gain, 0.1f);
    }
}
