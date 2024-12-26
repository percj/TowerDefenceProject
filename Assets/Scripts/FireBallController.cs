using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireBallController : MonoBehaviour
{
    public int Damage;
    List<Health> DamagedList;
    public GameObject hitAnimation;

    private void Start()
    {
        DamagedList = new List<Health>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CanTakeDamage")
        {
            Health health;
            if (other.TryGetComponent(out health) && !DamagedList.Contains(health))
                DamagedList.Add(health);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Health health;
        if (other.TryGetComponent(out health) && DamagedList.Contains(health))
            DamagedList.Remove(health);
    }
    public void hitEnemy()
    {
        if (DamagedList.Count  == 0) { Destroy(gameObject); return; }
        DamagedList.ForEach(x=> x.decreaseHealth(Damage));
        var x = Instantiate(hitAnimation, null);
        x.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
    
}
