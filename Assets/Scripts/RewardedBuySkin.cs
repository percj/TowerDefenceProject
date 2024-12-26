using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedBuySkin : MonoBehaviour
{
    public StoreItem BuyingItem;
    [SerializeField] Text buyItemPrice;
    [SerializeField] CurrencyManager currencyManager;
    [SerializeField] Image buttonColor;
    [SerializeField] SoundManager SoundManager;

    internal void UIRefresh()
    {
        buyItemPrice.text = BuyingItem.RewardedPrice.ToString();
        if (currencyManager.ControllMoney(BuyingItem.RewardedPrice))
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
        currencyManager.UseMoney(BuyingItem.RewardedPrice);
        BuyingItem.BuyThisItem();
        BuyingItem.EquipItem();
        SoundManager.PlayOneShot(SoundManager.Gain, 0.1f);
    }
}
