using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class carryObjects : MonoBehaviour
{
    [Range(1,100)]public int carryLimit;
    [HideInInspector]public int currCount;
    [SerializeField] public List<GameObject> carryObjets;
    [SerializeField] GameObject carryPrefeb;
    [SerializeField] Transform spawnPos;
    [Range(0, 10)][SerializeField] float objectSpacing;


    // Update is called once per frame
    void Update()
    {
    }

    public void addObject()
    {
        var x = Instantiate(carryPrefeb, transform);
        x.transform.parent = spawnPos;
        x.transform.position = spawnPos.position;
        x.transform.position += x.transform.up * objectSpacing * currCount ;
        carryObjets.Add(x);
        currCount = carryObjets.Count;
    }
    public void removeObject()
    {
        if (currCount == 0) return;
        var x = carryObjets.Last();
        carryObjets.Remove(x);
        Destroy(x);
        currCount = carryObjets.Count;
        x = null;
    }

}
