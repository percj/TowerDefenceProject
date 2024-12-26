using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerAllyCardController : MonoBehaviour
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
    public ButtonUIController buyController;
    public UnityEvent unityEvent; 
    public ButtonUIController buttonUIController;

    public rewardedTower rewardedTower;

    public GameObject bronze;
    public GameObject gold;
    public GameObject wood;
    public GameObject human;

    public void Start()
    {
        buyController.UIRefresh();
    }

    private void OnEnable()
    {
        buyController.UIRefresh();
    }

    private void OnDisable()
    {
        buyController.UIRefresh();
        buyButton.interactable = true;
        freebuyButton.interactable = true;
    }


    public void UIRefresh()
    {
        var towerController = buttonUIController.parentObject.GetComponent<TowerController>();
        var inventory = GameSingleton.Instance.Inventory;
        CardHeader.text = CardName.ToString();
        BronzeText.text = BronzeNeeded.ToString();
        GoldText.text = GoldNeeded.ToString();
        WoodText.text = WoodNeeded.ToString();
        HumanText.text = HumanNeeded.ToString();
        if (towerController.currArcherCount < towerController.totalArcherCount)
        {
            buyButton.interactable = true;
            freebuyButton.interactable = true;
        }
        else { 
            buyButton.interactable = false; 
            freebuyButton.interactable = false; 
        }
        BronzeText.color = Color.white; WoodText.color = Color.white; HumanText.color = Color.white; GoldText.color = Color.white;
        if (inventory.bronzeCount < BronzeNeeded) { BronzeText.color = Color.red; buyButton.interactable = false;  }
        if (inventory.woodCount < WoodNeeded){WoodText.color = Color.red; buyButton.interactable = false;}
        if (inventory.goldCount < GoldNeeded) { GoldText.color = Color.red; buyButton.interactable = false;  }
        if (inventory.soldierCount < HumanNeeded) { HumanText.color = Color.red; buyButton.interactable = false;  }

        if (BronzeNeeded == 0) bronze.SetActive(false);
        if (GoldNeeded == 0) gold.SetActive(false);
        if (WoodNeeded == 0) wood.SetActive(false);
        if (HumanNeeded == 0) human.SetActive(false);
    }

    public void BuyButton()
    {
        var inventory = GameSingleton.Instance.Inventory;
        if(inventory.bronzeCount >= BronzeNeeded && inventory.woodCount >= WoodNeeded && inventory.goldCount>= GoldNeeded 
            &&  inventory.soldierCount >= HumanNeeded)
        {
            inventory.bronzeCount -= BronzeNeeded;
            inventory.woodCount -= WoodNeeded;
            inventory.goldCount -= GoldNeeded;
            inventory.soldierCount -= HumanNeeded;
            BarrackManager.Instance.removeSoldier();
            inventory.UpdateText();
            unityEvent.Invoke();
            buyController.UIRefresh();
        }
    }

    public void watch()
    {
        BuyFree();
        /*rewardedTower.currEvent.AddListener(BuyFree);
        rewardedTower.UserChoseToWatchAd();*/
    }

    public void BuyFree()
    {
        unityEvent.Invoke();
        buyController.UIRefresh();
    }


}
