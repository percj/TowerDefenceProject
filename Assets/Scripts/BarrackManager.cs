using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrackManager : MonoBehaviour
{
    [SerializeField]List<BarrackAnimationManager> barracks;

    private static BarrackManager _instance;
    public static BarrackManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void removeSoldier()
    {
        var removedSoldier = barracks.Where(x => x.SoldierCount > 0).OrderBy(a => Guid.NewGuid()).FirstOrDefault();
        if (removedSoldier != null) removedSoldier.removeSoldier();
        GameSingleton.Instance.Inventory.RemoveSoldier(1);
    }

}
