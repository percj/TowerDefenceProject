using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HireHelper : MonoBehaviour
{

    [Header("=== Identifier ===")]
    public string ID;
    public string StationName;


    
    [SerializeField] List<GameObject> helpersPrefab;
    [SerializeField] List<HelperController> helpers;
    [SerializeField] Transform helperParent;
    [SerializeField] Transform startPos;
    [SerializeField] Transform waitingPos;
    [SerializeField] Transform CollectPos;
    [SerializeField] CustomerSpawner Spawner;

    int SpeedLevel=1;
    int CollectSpeedLevel=1;
    int CapacityLevel=1;

    public float TotalMoney;
    bool canGiveMoney;
    [HideInInspector]public float investedPrice;
    [SerializeField] Image MoneyFiller;
    [SerializeField] TextMeshProUGUI MoneyText;
    void Start()
    {
        LoadData();
        refreshMoney();
    }

    private void LoadData()
    {               
        var helpersCount = PlayerPrefs.GetInt(ID + StationName + "helpersCount", 0);
        for (int i = 0; i < helpersCount; i++) HireEmployee(true);
        investedPrice = PlayerPrefs.GetFloat(ID + StationName + "investedPrice", 0);
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(ID + StationName + "investedPrice", investedPrice);
        PlayerPrefs.SetInt(ID + StationName + "helpersCount", helpers.Count);
    }

    void refreshMoney()
    {
        
        MoneyFiller.fillAmount = investedPrice / TotalMoney;
        MoneyText.text = (TotalMoney - investedPrice).ToString();
        SaveData();
    }

    public float Payment(float givenPrice)
    {
        if (canGiveMoney)
        {
            if (TotalMoney - investedPrice > 0)
            {
                investedPrice += givenPrice;
                refreshMoney();
                if (TotalMoney - investedPrice <= 0)
                    HireEmployee(false);
                
                return -givenPrice;
            }
            else
            {
                HireEmployee(false);
                return -(TotalMoney - investedPrice);
            }
        }
        else return 0;
       
    }
    void HireEmployee(bool isLoad)
    {
        var helper = helpersPrefab[Random.Range(0, helpersPrefab.Count)];
        var x = Instantiate(helper, helperParent);
        x.transform.position = startPos.position;
        x.transform.parent = helperParent;
        var helperConroller = x.GetComponent<HelperController>();
        helperConroller.customerSpawner = Spawner;
        helperConroller.Collect = CollectPos;
        helperConroller.WaitPos = waitingPos;
        helperConroller.setSpeedLevel(SpeedLevel);
        helperConroller.setCapacityLevel(CapacityLevel);
        helperConroller.setCollectSpeedLevel(CollectSpeedLevel);
        helpers.Add(helperConroller);
        investedPrice = 0;
        refreshMoney();
        canGiveMoney = false;
        if (!isLoad) GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.OpenStation);





    }
    public void setHelperSpeedLevel(int currLevel)
    {
        SpeedLevel = currLevel;
        foreach (var helper in helpers)
            helper.setSpeedLevel(SpeedLevel);
    }

    internal void setHelperCollectSpeedLevel(int currLevel)
    {
        CollectSpeedLevel = currLevel;
        foreach (var helper in helpers)
            helper.setCollectSpeedLevel(CollectSpeedLevel);
    }

    internal void setHelperCapacityLevel(int currLevel)
    {
        CapacityLevel = currLevel;
        foreach (var helper in helpers)
            helper.setCapacityLevel(CapacityLevel);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canGiveMoney = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canGiveMoney = true;
        }
    }
}
