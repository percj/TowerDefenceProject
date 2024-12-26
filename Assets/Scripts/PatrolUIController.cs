using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolUIController : MonoBehaviour
{
    [HideInInspector]public GameObject parentObject;
    [SerializeField] List<PatrollAllyCardController> cards;

    public void ParentCloser()
    {
        parentObject.GetComponent<PatrolAreaUIOpenner>().ClearUI();
    }
    public void UIRefresh()
    {
        cards.ForEach(x => x.UIRefresh());
    }
    public void OnClick()
    {
        Destroy(parentObject);
        gameObject.SetActive(false);
    }

    public void AddSoldierLevel1ToPatrol()
    {
        parentObject.GetComponent<PatrolAreaUIOpenner>().AddSoldierLevel1();
        UIRefresh();
    }
    public void AddSoldierLevel2ToPatrol()
    {
        parentObject.GetComponent<PatrolAreaUIOpenner>().AddSoldierLevel2();
        UIRefresh();
    }
    public void AddSoldierLevel3ToPatrol()
    {
        parentObject.GetComponent<PatrolAreaUIOpenner>().AddSoldierLevel3();
        UIRefresh();
    }
    public void AddSoldierLevel4ToPatrol()
    {
        UIRefresh();
        parentObject.GetComponent<PatrolAreaUIOpenner>().AddSoldierLevel4();
    }
    public void AddSoldierLevel5ToPatrol()
    {
        UIRefresh();
        parentObject.GetComponent<PatrolAreaUIOpenner>().AddSoldierLevel5();
    }
}
