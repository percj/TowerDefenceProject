using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAllyController : MonoBehaviour
{
    public bool isCommander;
    public bool isFollower;
    public bool isReturninGroup;
    public PatrolGroupController patrolGroup;
    public NavMeshAgent agent;
    public bool isAttacking;
    public Health target;
    public float damage;
    public Health health;
    NavMeshPath navMeshPath;
    public GameObject formassion;
    public Animator anim;
    public NearAttackController nearAttackController;
    public bool isDying;
    public AudioSource audioSource;
    private Vector3 previousPosition;
    public float curSpeed;
    private void Awake()
    {
       navMeshPath = new NavMeshPath();
       Debug.Log(agent.CalculatePath(transform.position, navMeshPath));
    }
    void LateUpdate()
    {
        if (!isDying ) AnimationControll();
    }

    void AnimationControll()
    {
        if (health.isDead)
        {
            isDying = true;
            if (isCommander)
            {
                patrolGroup.patrolGroup.Remove(this);
                patrolGroup.SetCommenderSettings();
            }
            else
            {
                patrolGroup.patrolGroup.Remove(this);
            }
                anim.SetBool("run", false);
            anim.SetBool("attack", false);
            health.healtBar.SetActive(false);
            Destroy(agent);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(2);
            sequence.Append(transform.DOMoveY(transform.position.y - 2, 3));
            sequence.OnComplete(() => { Destroy(gameObject); });
        }
        else
        {
            Vector3 curMove = transform.position - previousPosition;
            curSpeed = curMove.magnitude / Time.deltaTime;
            previousPosition = transform.position;
            if(isAttacking)
                anim.SetFloat("Speed", curSpeed/agent.speed);
            else
                anim.SetFloat("Speed", curSpeed/2 / agent.speed);

            if(isAttacking)
            {
                tryToGoTarget();
            }
            if (Vector3.Distance(transform.position, agent.destination) < 2f)
            {
                anim.SetBool("run", false);
                anim.SetBool("attack", isAttacking);
                agent.destination = transform.position;
                transform.LookAt(agent.destination);
                transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            else
            {
                anim.SetBool("run", true);
                anim.SetBool("attack", false);
            }
        }
    }
    
    public void WalkSound()
    {
        audioSource.PlayOneShot(GameSingleton.Instance.Sounds.armourWalk);
    }
    public void damageWithSword()
    {
        if(target != null)
        target.GetComponentInChildren<Health>().decreaseHealth(damage);
    }
    public void tryToGoTarget()
    {
        if(target == null)
            nearAttackController.FindTargetForPatrol();
        if (health.isDead) return;
        if (target.GetComponent<Health>().isDead)
        {
            nearAttackController.FindTargetForPatrol();
        }
        else
            agent.SetDestination(target.transform.position);
    }

}
