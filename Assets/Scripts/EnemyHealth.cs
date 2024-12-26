using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public Animator animator;
    public override void decreaseHealth(float damage)
    {
        base.decreaseHealth(damage);
        if (CurrentHealth <= 0)
        {
            if (Enemy.Instance.enemyList.Contains(destroyObject))
            { 
                Enemy.Instance.enemyList.Remove(destroyObject);
                if (Enemy.Instance.enemyList.Count == 0) 
                    GameSingleton.Instance.levelManager.finished();
            }
            animator.SetInteger("DeadCount", Random.Range(1, 3));
            animator.SetBool("Dead",true);
        }
        animator.SetTrigger("Hit");
    }
}
