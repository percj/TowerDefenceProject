using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyAreaController : MonoBehaviour
{
    [SerializeField] StationController station;
    public float moneyAmount;
    [HideInInspector]public List<GameObject> moneys;
    [SerializeField] Transform moneyPos;
    [Range(0,5)] [SerializeField] float ObjectSpacingHeight;
    [SerializeField] GameObject objectPrefab;

    private void Awake()
    {
        LoadData();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            addMoney();
    }

    private void LoadData()
    {                                           // 1CustomerStationisUnlocked
        var moneysCount = PlayerPrefs.GetInt(station.ID + station.StationName + "moneysCount", 0);
        for (int i = 0; i < moneysCount; i++) addMoney();

    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(station.ID + station.StationName + "moneysCount", moneys.Count);
    }

    public void addMoney()
    {
        var x = Instantiate(objectPrefab, moneyPos);
        x.transform.parent = moneyPos;
        x.transform.position += transform.up * moneys.Count * ObjectSpacingHeight;
        moneys.Add(x); 
        SaveData();
    }
 
    public float removeMoney()
    {
        var x = moneys.Last();
        moneys.Remove(x);
        Destroy(x);
        x = null;
        SaveData();
        return moneyAmount;
    }
}
