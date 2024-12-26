using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PatrollAllyCardController : MonoBehaviour
{

    public string CardName;
    public int BronzeNeeded;
    public int WoodNeeded;
    public int GoldNeeded;
    public int HumanNeeded;
    public TextMeshProUGUI BronzeText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI HumanText;
    public TextMeshProUGUI CardHeader;
    public Button buyButton;
    public Button freebuyButton;
    public UnityEvent unityEvent;
    public PatrolUIController patrolUIController;
    

    public rewardedPatrol rewardedPatrol;

    public GameObject bronze;
    public GameObject gold;
    public GameObject wood;
    public GameObject human;

    public void Start()
    {
        patrolUIController.UIRefresh();
    }

    private void OnEnable()
    {
        patrolUIController.UIRefresh();
    }

    private void OnDisable()
    {
        patrolUIController.UIRefresh();
        buyButton.interactable = true;
        freebuyButton.interactable = true;
    }

    public void UIRefresh()
    {
        var patrolAreaUIOpenner = patrolUIController.parentObject.GetComponent<PatrolAreaUIOpenner>();
        var inventory = GameSingleton.Instance.Inventory;
        CardHeader.text = CardName.ToString();
        BronzeText.text = BronzeNeeded.ToString();
        GoldText.text = GoldNeeded.ToString();
        WoodText.text = WoodNeeded.ToString();
        HumanText.text = HumanNeeded.ToString();
        if (patrolAreaUIOpenner.soldierCount < patrolAreaUIOpenner.MaxSoldierCount)
        {
            buyButton.interactable = true;
            freebuyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
            freebuyButton.interactable = false;
        }
        BronzeText.color = Color.white; WoodText.color = Color.white; HumanText.color = Color.white; GoldText.color = Color.white;
        if (inventory.bronzeCount < BronzeNeeded) { BronzeText.color = Color.red; buyButton.interactable = false; }
        if (inventory.woodCount < WoodNeeded) { WoodText.color = Color.red; buyButton.interactable = false; }
        if (inventory.goldCount < GoldNeeded) { GoldText.color = Color.red; buyButton.interactable = false; }
        if (inventory.soldierCount < HumanNeeded) { HumanText.color = Color.red; buyButton.interactable = false; }

        if (BronzeNeeded == 0) bronze.SetActive(false);
        if (GoldNeeded == 0) gold.SetActive(false);
        if (WoodNeeded == 0) wood.SetActive(false);
        if (HumanNeeded == 0) human.SetActive(false);
    }
        public void BuyButton()
    {
        var inventory = GameSingleton.Instance.Inventory;
        if (inventory.bronzeCount >= BronzeNeeded && inventory.woodCount >= WoodNeeded && inventory.goldCount >= GoldNeeded
            && inventory.soldierCount >= HumanNeeded)
        {
            inventory.bronzeCount -= BronzeNeeded;
            inventory.woodCount -= WoodNeeded;
            inventory.goldCount -= GoldNeeded;
            inventory.soldierCount -= HumanNeeded;
            BarrackManager.Instance.removeSoldier();
            inventory.UpdateText();
            unityEvent.Invoke();
        }
        patrolUIController.UIRefresh();
    }

    public void watch()
    {
        rewardedPatrol.currEvent.AddListener(BuyFree);
        rewardedPatrol.UserChoseToWatchAd();
    }

    public void BuyFree()
    {
        /*Add Ads*/
        unityEvent.Invoke();
        patrolUIController.UIRefresh();
    }

}
