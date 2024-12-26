
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIController : MonoBehaviour
{
     public GameObject parentObject;

    [SerializeField] List<TowerAllyCardController> cards;

    public void UIRefresh()
    {
        cards.ForEach(x => x.UIRefresh());
    }
  

    public void addBronzeArcherToTower()
    {
        parentObject.GetComponent<TowerController>().AddBronzeArcher();
    }

    public void AddGoldArcherToTower()
    {
        parentObject.GetComponent<TowerController>().AddGoldArcher();
    }

    public void AddLongBowArcherToTower()
    {
        parentObject.GetComponent<TowerController>().AddLongBowArcher();
    }

    public void AddShortBowArcherToTower()
    {
        parentObject.GetComponent<TowerController>().AddShortBowArcher();
    }

    public void AddMageArcherToTower()
    {
        parentObject.GetComponent<TowerController>().AddMageArcher();
    }

}