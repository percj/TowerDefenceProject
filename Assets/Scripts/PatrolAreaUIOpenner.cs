using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class PatrolAreaUIOpenner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SoldierCount;
    [SerializeField] Image Filler;
    public float TotalTime;
    [HideInInspector] public float CurrTime;
    public bool isOpened = false;
    public int soldierCount;
    public int MaxSoldierCount = 5;


    [SerializeField] PatrolGroupController PatrolGroup;
    [SerializeField] GameObject SoldierLevel1;
    [SerializeField] GameObject SoldierLevel2;
    [SerializeField] GameObject SoldierLevel3;
    [SerializeField] GameObject SoldierLevel4;
    [SerializeField] GameObject SoldierLevel5;

    void Start()
    {
        refreshUI();
    }

    void refreshUI()
    {
        Filler.fillAmount = CurrTime / TotalTime;
        SoldierCount.text= soldierCount+" / 5";
    }
    public void ClearUI()
    {
        CurrTime = 0;
        refreshUI();
        isOpened = false;
    }

    public void TimerDecrease(int time)
    {
        if (soldierCount >= MaxSoldierCount) return;
        if (TotalTime - CurrTime > 0 && !isOpened )
        {
            CurrTime += time*0.01f;
            refreshUI();
        }
        else
        {
            GameSingleton.Instance.UI.PatrolSoldierOpen.parentObject = gameObject;
            isOpened = true;
            CurrTime = 0;
            GameSingleton.Instance.UI.PatrolSoldierOpen.gameObject.SetActive(true);
        }
    }

    public void AddSoldierLevel1()
    {
        PatrolGroup.AddSoldierToPatrol(SoldierLevel1.GetComponent<PatrolAllyController>());
        soldierCount++;
        refreshUI();
    }

    public void AddSoldierLevel2()
    {
        PatrolGroup.AddSoldierToPatrol(SoldierLevel2.GetComponent<PatrolAllyController>());
        refreshUI();
        soldierCount++;
    }
    public void AddSoldierLevel3()
    {
        PatrolGroup.AddSoldierToPatrol(SoldierLevel3.GetComponent<PatrolAllyController>());
        refreshUI();
        soldierCount++;
    }

    public void AddSoldierLevel4()
    {
        PatrolGroup.AddSoldierToPatrol(SoldierLevel4.GetComponent<PatrolAllyController>());
        refreshUI();
        soldierCount++;
    }

    public void AddSoldierLevel5()
    {
        PatrolGroup.AddSoldierToPatrol(SoldierLevel5.GetComponent<PatrolAllyController>());
        refreshUI();
        soldierCount++;
    }

}
