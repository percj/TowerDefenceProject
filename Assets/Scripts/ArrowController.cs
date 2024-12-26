using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Health Target;
    public int Damage;
    public GameObject hitAnimation;

    public void hitEnemy()
    {
        if(Target == null) { Destroy(gameObject); return; }
        Target.decreaseHealth(Damage);
        var x = Instantiate(hitAnimation,null);
        x.transform.localScale = Vector3.one/2;
        x.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }


}
