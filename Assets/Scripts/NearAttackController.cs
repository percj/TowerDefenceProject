using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class NearAttackController : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] public EnemyAI enemy;
    [SerializeField] public PatrolAllyController patrol;
    [SerializeField] public List<Health> TargetList;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != gameObject.layer && other.tag == "NearAttack")
        {
            if (enemy != null)
            {
                var targetHealth = other.GetComponent<NearAttackController>().health;
                if (!TargetList.Contains(targetHealth)&& !targetHealth.isDead)
                TargetList.Add(targetHealth);
                if(!enemy.nearAttack)
                {
                    enemy.nearAttackTarget = targetHealth;
                    enemy.nearAttack = true;
                }
                else
                {
                    enemy.nearAttackTarget = TargetList.Where(x => x != null).OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
                }
            }
            if (patrol != null && other.GetComponent<NearAttackController>().enemy.inTown)
            {
                var targetHealth = other.GetComponent<NearAttackController>().health;
                if (!TargetList.Contains(targetHealth))
                    TargetList.Add(targetHealth);
                if(!patrol.isAttacking)
                {
                    patrol.target = targetHealth;
                    patrol.isAttacking = true;
                }
                else
                {
                    patrol.target = TargetList.Where(x => x != null).OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
                }

            }
        }
       
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer && other.tag == "NearAttack")
        {
            if (enemy != null && enemy.nearAttackTarget == other.GetComponent<NearAttackController>().health)
            {
                TargetList.Remove(other.GetComponent<NearAttackController>().health);
                if (TargetList.Count == 0)
                {
                    enemy.nearAttackTarget = null;
                    //enemy.targetPos = null;
                    enemy.nearAttack = false;
                }
                else
                {
                    enemy.nearAttackTarget = TargetList.First();
                    enemy.nearAttack = true;
                }
            }
            if (patrol != null && patrol.target == other.GetComponent<NearAttackController>().health)
            {
                TargetList.Remove(other.GetComponent<NearAttackController>().health);
                if (TargetList.Count == 0)
                {
                    patrol.target = null;
                    patrol.isAttacking = false;
                }
                else
                {
                    patrol.target = TargetList.First();
                    patrol.isAttacking = true;
                }


            }
        }
    }

    public void FindTargetForPatrol()
    {
        if(TargetList.Contains(TargetList.FirstOrDefault())) 
            TargetList.Remove(TargetList.First());
        if (TargetList.Count == 0)
        {
            patrol.target = null;
            patrol.isAttacking = false;
        }
        else
        {
            patrol.target = TargetList.First();
            patrol.isAttacking = true;
        }
    }
    public void FindTargetForEnemey()
    {
        TargetList.Remove(TargetList.First());
        if (TargetList.Count == 0)
        {
            enemy.nearAttackTarget = null;
            //enemy.targetPos = null;
            enemy.nearAttack = false;
        }
        else
        {
            enemy.nearAttackTarget = TargetList.First();
            enemy.nearAttack = true;
        }
    }
}
