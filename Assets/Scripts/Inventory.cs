using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int woodCount;
    public int goldCount;
    public int bronzeCount;
    public int soldierCount;

    public TextMeshProUGUI woodText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI bronzeText;
    public TextMeshProUGUI soldierText;
    public GameObject curencyPanel;
    public GameObject soldierPanel;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        woodText.text = woodCount.ToString();
        goldText.text = goldCount.ToString();
        bronzeText.text = bronzeCount.ToString();
        soldierText.text = soldierCount.ToString();

        if (soldierCount == 0)
            soldierPanel.SetActive(false);
        else
            soldierPanel.SetActive(true);

        if (woodCount == 0) woodText.gameObject.transform.parent.gameObject.SetActive(false);
        else woodText.gameObject.transform.parent.gameObject.SetActive(true);
        if (goldCount == 0) goldText.gameObject.transform.parent.gameObject.SetActive(false);
        else goldText.gameObject.transform.parent.gameObject.SetActive(true);
        if (bronzeCount == 0) bronzeText.gameObject.transform.parent.gameObject.SetActive(false);
        else bronzeText.gameObject.transform.parent.gameObject.SetActive(true);
        if (woodCount == 0 && goldCount == 0 && bronzeCount == 0) curencyPanel.SetActive(false);
        else curencyPanel.SetActive(true);
    }
 
    public void AddMaterial(MaterialType type,int Count)
    {
        switch(type)
        {
            case MaterialType.Wood:
                woodCount += Count;
                break;
            case MaterialType.Gold:
                goldCount += Count;
                break;
            case MaterialType.Bronze:
                bronzeCount += Count;
                break;
        }
        UpdateText();
    }

    internal void RemoveMaterial(MaterialType type, int Count)
    {
        switch (type)
        {
            case MaterialType.Wood:
                woodCount -= Count;
                break;
            case MaterialType.Gold:
                goldCount -= Count;
                break;
            case MaterialType.Bronze:
                bronzeCount -= Count;
                break;
        }
        UpdateText();
    }

    internal void RemoveSoldier(int Count)
    {
        soldierCount -= Count;
        UpdateText();
    }
    internal void AddSoldier(int Count)
    {
        soldierCount += Count;
        UpdateText();
    }
}
