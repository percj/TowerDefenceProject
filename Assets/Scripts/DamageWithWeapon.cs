using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWithWeapon : MonoBehaviour
{
    HashSet<Health> damagedList;
    [SerializeField] AudioSource Audio;

    private void Start()
    {
        damagedList = new HashSet<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CanTakeDamage")
        {
            EnemyAI enemyAI;
            if(other.transform.parent.TryGetComponent(out enemyAI))
            {
                damagedList.Add(enemyAI.health);
            }
        }
    }
    public void damageWithSword(float damage)
    {
        foreach (Health health in damagedList)
        {
            health.decreaseHealth(damage);
            Audio.PlayOneShot(GameSingleton.Instance.Sounds.Sword,.2f);
            Audio.PlayOneShot(GameSingleton.Instance.Sounds.Hit, .2f);
        }
        damagedList.Clear();
    }
}
