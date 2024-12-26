using System;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class LifeManager: MonoBehaviour
{
    public int MaxLife;
    public List<GameObject> LifeList;
    public PlayerLogic playerLogic;
    private void Start()
    {
        updateUI();
    }
    private void updateUI()
    {
        LifeList.ForEach(x => x.SetActive(false));
        for (int i = 0; i < MaxLife; i++)
        {
            LifeList[i].SetActive(true);
        }
    }
    public bool decreaseLife()
    {
        MaxLife--;
        updateUI();
        if (MaxLife <= 0) 
            return false;
        playerLogic.Respawn();
        return true;
    }

}