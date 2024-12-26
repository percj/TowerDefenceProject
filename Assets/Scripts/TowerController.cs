using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerController : MonoBehaviour
{
    public int totalArcherCount;
    [HideInInspector]public float investedPrice;
    public int currArcherCount;
    public List<GameObject> archerPos;
    public GameObject Smoke;
    public GameObject BronzeArcher;
    public GameObject GoldArcher;
    public GameObject LongBowArcher;
    public GameObject ShortBowArcher;
    public GameObject TowerMage;
    public TextMeshProUGUI Counter;

    private void Awake()
    {
        UIRefresh();
    }
    void UIRefresh()
    {
        Counter.text = currArcherCount + "/" + totalArcherCount;
    }

    public void AddBronzeArcher()
    {
        if(currArcherCount < totalArcherCount)
        {
            var x = Instantiate(BronzeArcher, archerPos[currArcherCount].transform);
            x.transform.position = archerPos[currArcherCount].transform.position;
            var y = Instantiate(Smoke, archerPos[currArcherCount].transform);
            y.transform.localScale = Vector3.one * 4;
            currArcherCount++;
            UIRefresh();
        }
    }
    public void AddGoldArcher()
    {
        if (currArcherCount < totalArcherCount)
        {
            var x = Instantiate(GoldArcher, archerPos[currArcherCount].transform);
            x.transform.position = archerPos[currArcherCount].transform.position;
            var y = Instantiate(Smoke, archerPos[currArcherCount].transform);
            y.transform.localScale = Vector3.one * 4;
            currArcherCount++;
            UIRefresh();
        }
    }

    public void AddLongBowArcher()
    {
        if (currArcherCount < totalArcherCount)
        {
            var x = Instantiate(LongBowArcher, archerPos[currArcherCount].transform);
            x.transform.position = archerPos[currArcherCount].transform.position;
            var y = Instantiate(Smoke, archerPos[currArcherCount].transform);
            y.transform.localScale = Vector3.one * 4;
            currArcherCount++;
            UIRefresh();
        }
    }

    public void AddShortBowArcher()
    {
        if (currArcherCount < totalArcherCount)
        {
            var x = Instantiate(ShortBowArcher, archerPos[currArcherCount].transform);
            x.transform.position = archerPos[currArcherCount].transform.position;
            var y = Instantiate(Smoke, archerPos[currArcherCount].transform);
            y.transform.localScale = Vector3.one * 4;
            currArcherCount++;
            UIRefresh();
        }
    }

    public void AddMageArcher()
    {
        if (currArcherCount < totalArcherCount)
        {
            var x = Instantiate(TowerMage, archerPos[currArcherCount].transform);
            x.transform.position = archerPos[currArcherCount].transform.position;
            var y = Instantiate(Smoke, archerPos[currArcherCount].transform);
            y.transform.localScale = Vector3.one * 4;
            currArcherCount++;
            UIRefresh();
        }
    }

}
