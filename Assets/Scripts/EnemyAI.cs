using DG.Tweening;
using Lofelt.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    public Health health;
    NavMeshAgent agent;
    [SerializeField] DestroyableObject Character;
    Animator anim;
    [SerializeField] Castle castle;
    public GameObject targetPos;
    GameObject target;
    public Health nearAttackTarget;
    [SerializeField] GameObject TownObject;
    [SerializeField] List<GameObject> TownObjectAttackPosList;
    NavMeshPath navMeshPath;
    public NearAttackController nearAttackController;
    List<GameObject> FirstDestroyableObject;
    public bool nearAttack;
    public bool isDying;
    public bool inTown;
    [SerializeField] AudioSource Audio;

    bool isGameStart;

    private void AtStart()
    {
       isGameStart = true;
        anim.SetBool("run", true);
       if(!tryToGoTown())
        {
            targetPos = findTarget();
            if(targetPos!= null)
            agent.destination = targetPos.transform.position;
        }
    }

    void Start()
    {
        FirstDestroyableObject = new List<GameObject>();
        navMeshPath = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Invoke("AtStart", 2);
    }
  
    public bool tryToGoTown()
    {
        if(inTown)
        {
            targetPos = TownObjectAttackPosList.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
            agent.SetDestination(targetPos.transform.position);
            target = castle.gameObject;
            return true;
        }
        if (health.isDead || nearAttack) return false;
        if (FirstDestroyableObject.Any(x => x.GetComponent<Health>().isDead) && agent.CalculatePath(TownObjectAttackPosList.First().transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            inTown = true; 
            targetPos = TownObjectAttackPosList.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
            agent.destination = targetPos.transform.position;
            target = castle.gameObject;
            return true;
        }
        else
            targetPos = null;
        return false;
    }

    GameObject findTarget()
    {
        target = castle.destroyableObjects.Where(x => x != null).OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
        targetPos = target;
        return target;
    }



    // Update is called once per frame
    void LateUpdate()
    {

        if(!isDying && isGameStart) control();
    }

    private void control()
    {
       
        if (health.isDead)
        {
            isDying = true;
            anim.SetBool("run", false);
            anim.SetBool("attack", false);
            health.healtBar.SetActive(false);
            Destroy(agent);
            Outline outline;
            gameObject.TryGetComponent(out outline);
            if(outline != null) Destroy(outline);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(2);
            sequence.Append(transform.DOMoveY(transform.position.y -10, 3));
            sequence.OnComplete(() => { /*Destroy(gameObject);*/});
        }
        else if (nearAttack && nearAttackTarget != null && !nearAttackTarget.isDead)
        {
            if (agent.CalculatePath(nearAttackTarget.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                agent?.SetDestination(nearAttackTarget.transform.position);
                //targetPos = nearAttackTarget.gameObject;
                ControllDistance(nearAttackTarget.transform);
            }
            else
            {
                nearAttack = false;
                nearAttackTarget = null;
            }
        }
        else
        {
            if (targetPos == null)
            {
                if (!tryToGoTown())
                    targetPos = findTarget();
            }
            else
            {
                ControllDistance(targetPos.transform);
            }
        }
        
    }

    public bool ControllDistance(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < 2f)
        {
            anim.SetBool("run", false);
            anim.SetBool("attack", true);
            agent.destination = transform.position;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
            return true;
        }
        else
        {
            if (target != null && agent.destination != target.transform.position) agent.destination = target.transform.position;
            anim.SetBool("run", true);
            anim.SetBool("attack", false);
            return false;
        }
    }
    public void FootSteps()
    {
        Audio.PlayOneShot(GameSingleton.Instance.Sounds.FootStep);
    }
    public void hit()
    {
        if (agent == null) return;
        if(nearAttackTarget != null)
        {
            if (ControllDistance(nearAttackTarget.transform))
            {
                nearAttackTarget.GetComponentInChildren<Health>().decreaseHealth(Character.Damage);
                if (nearAttackTarget.GetComponentInChildren<Health>().isDead)
                    nearAttackController.FindTargetForEnemey();
            }
               
        }
        else
        {
            if (targetPos != null && ControllDistance(targetPos.transform))
            {
                target.GetComponentInChildren<Health>().decreaseHealth(Character.Damage);
                if (target.GetComponentInChildren<Health>().isDead)
                    tryToGoTown();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FirstDestroyableObject")
        {
            FirstDestroyableObject.Add(other.gameObject);
            tryToGoTown();
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FirstDestroyableObject")
        {
            FirstDestroyableObject.Remove(other.gameObject);
        }
        if(other.tag == "Wall")
            tryToGoTown();
    }

}
