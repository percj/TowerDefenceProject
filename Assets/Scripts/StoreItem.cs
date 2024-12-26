using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StoreItemType
{
    Body,Head
}
public class StoreItem : MonoBehaviour
{
    [SerializeField] string ID;
    [SerializeField] string Name;
    [SerializeField] SkinController skinController;
    [SerializeField] GameObject EquipTick;
    [SerializeField] GameObject UnBuyed;
    [SerializeField] StoreItemType ItemType;
    public int RewardedPrice;
    public int ItemPrice;
    [SerializeField] RewardedBuySkin RewardedButton;
    [SerializeField] BuySkin BuyButton;
    [SerializeField] AllStoreController allStoreController;
    [SerializeField] MenuController menuController;
    public string Equip;
    public string Buyed;

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(ID+Name+"Equip", Equip);
        PlayerPrefs.SetString(ID+Name+ "Buyed", Buyed);
    }

    public void LoadData()
    {
        Equip = PlayerPrefs.GetString(ID + Name + "Equip", Equip);
        Buyed = PlayerPrefs.GetString(ID + Name + "Buyed", Buyed);

        if (Equip == "True") EquipTick.SetActive(true);
        else EquipTick.SetActive(false);

        if (Buyed == "True") UnBuyed.SetActive(false);
        else UnBuyed.SetActive(true);
    }
    public void EquipItem()
    {
        menuController.StoreSelectedClick();
        allStoreController.UIRefresh(ItemType);
        Equip = "True";
        SaveData();
        skinController.EquipItem(ItemType, Name);
        EquipTick.SetActive(true);
    }

    public void OpenBuyingPanel()
    {
        BuyButton.BuyingItem = this;
        BuyButton.UIRefresh();

        RewardedButton.BuyingItem = this;
        RewardedButton.UIRefresh();


        RewardedButton.gameObject.SetActive(true);
        BuyButton.gameObject.SetActive(true);
    }

    internal void BuyThisItem()
    {
        Buyed = "True";
        UnBuyed.SetActive(false);
        SaveData();
    }
}
