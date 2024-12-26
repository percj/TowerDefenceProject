using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackAnimationManager : MonoBehaviour
{
    public int SoldierCount;
    [SerializeField] Animator animator;
    void Start()
    {
        SoldierCount = 5;
    }

    public void removeSoldier()
    {
        SoldierCount--;
        animator.SetInteger("SoldierCount", SoldierCount);
    }
    public void addSoldier()
    {
        SoldierCount++;
        animator.SetInteger("SoldierCount", SoldierCount);
    }
}
