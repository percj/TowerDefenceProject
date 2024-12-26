using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHealth : Health
{
    [SerializeField] levelsNeeds levelsNeeds;
    public override void decreaseHealth(float damage)
    {
        base.decreaseHealth(damage);
        levelsNeeds.TowerFiller.fillAmount = CurrentHealth / destroyableObject.MaxHealth;
        if (CurrentHealth <= 0)
        {
            GameSingleton.Instance.levelManager.failed();
        }
    }
}
