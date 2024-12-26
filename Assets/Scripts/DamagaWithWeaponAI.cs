using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagaWithWeaponAI : MonoBehaviour
{
    internal bool canDamage;
    [SerializeField] DestroyableObject character;
    HashSet<Health> DamagedObjects;

    private void Start()
    {
        DamagedObjects = new HashSet<Health>();
    }
    public void damageEnemy()
    {
       foreach (var health in DamagedObjects)
        {
            health.decreaseHealth(character.Damage);
        }
    }
    public void clearEnemyList()
    {
        DamagedObjects.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && canDamage)
        {
            Health health;
            if (other.transform.TryGetComponent(out health))
            {
                DamagedObjects.Add(health);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 )
        {
            Health health;
            if (other.transform.TryGetComponent(out health))
            {
                DamagedObjects.Remove(health);
            }
        }
    }
}
