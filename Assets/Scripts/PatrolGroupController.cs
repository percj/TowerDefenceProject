using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PatrolGroupController : MonoBehaviour
{
    public List<PatrolAllyController> patrolGroup;
    [HideInInspector] public GameObject formationLists;
    [HideInInspector] public List<GameObject> formations;
    [SerializeField] List<PatrolPoint> patrolList;
    [HideInInspector]public PatrolPoint NextPatrolPoint;
    [HideInInspector]public PatrolPoint LastPatrolPoint;
    public bool isGroupAttack;
    bool isPatrolStarting;
    bool isWaiting;
    bool inAllGroupFormationPos;

    bool isGameStart;

    private void AtStart()
    {
        isGameStart = true;
    }

    private void Start()
    {
        SetCommenderSettings();
    }
    
    public void AddSoldierToPatrol(PatrolAllyController soldier)
    {
        AtStart();
        soldier.patrolGroup = this;
        var go = Instantiate(soldier, transform);
        patrolGroup.Add(go);
        SetCommenderSettings();
        go.transform.parent = transform;
        go.transform.position = transform.position;
    }

    public void SetCommenderSettings()
    {
        if (patrolGroup.Count == 0) return;
        foreach (var member in patrolGroup)
        {
            member.isFollower = true;
        }

        patrolGroup[0].isFollower = false;
        patrolGroup[0].isCommander = true;
        patrolGroup[0].agent.speed = patrolGroup[0].agent.speed + 0.2f;
        setFormation(patrolGroup[0].formassion);
    }

    public void setFormation(GameObject newFormationParent)
    {
        formationLists = newFormationParent;
        formations.Clear();
        foreach(Transform item in formationLists.transform)
        {
            formations.Add(item.gameObject);
        }
    }

    private void Update()
    {
        if (!isGameStart || patrolGroup.Count == 0) return;
        if (!isGroupAttack && !inAllGroupFormationPos)
            StartCoroutine(WaitingForPatrol());
        else if (!isPatrolStarting && !isGroupAttack)
            StartCoroutine(startPatroling());
        else
        {
            var hasTargetPatrol = patrolGroup.Where(x => x.isAttacking).FirstOrDefault();
            if (hasTargetPatrol != null)
            {
                isGroupAttack = true;
                var hasNonTargetGroupMembers = patrolGroup.Where(x => !x.isAttacking).ToList();
                hasNonTargetGroupMembers.ForEach(x => { x.isAttacking = true; x.target = hasTargetPatrol.target.GetComponent<Health>(); x.tryToGoTarget(); });
            }
            else
                isGroupAttack = false;
        }

    }


    IEnumerator startPatroling()
    {
        isPatrolStarting = true;
        while (!isGroupAttack) 
        {
            var x = patrolGroup[0];
            if (x.agent.destination != NextPatrolPoint.transform.position)
            {
                isPatrolStarting = true;
                x.agent.SetDestination(NextPatrolPoint.transform.position);
            } 
            if(Vector3.Distance (x.agent.destination ,x.transform.position)<2 )
            {
                if (LastPatrolPoint == null)
                {
                    LastPatrolPoint = NextPatrolPoint;
                    NextPatrolPoint = NextPatrolPoint.connectedPoints.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();
                }
                else
                {
                    var patrolPoint = NextPatrolPoint.connectedPoints.Where(x => x != LastPatrolPoint).OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();
                    LastPatrolPoint = NextPatrolPoint;
                    NextPatrolPoint = patrolPoint;
                }
            }
            followCommander();
            yield return null; 
        }
        isPatrolStarting=false;
        inAllGroupFormationPos = false;
    }

    IEnumerator WaitingForPatrol()
    {
        if(isWaiting) yield break;
        isWaiting = true;
        while (!followCommander()) {
            yield return null; 
        }
        FindNearPoint(); 
        inAllGroupFormationPos = true;
        isWaiting = false;
    }

    bool followCommander()
    {
        patrolGroup.Where(x=> x.isFollower).ToList().ForEach(x => {
            if (!x.isAttacking && x.agent.destination != formations[patrolGroup.IndexOf(x)].transform.position)
            {

                x.isReturninGroup = true;
                x.agent.SetDestination(formations[patrolGroup.IndexOf(x)].transform.position);
            } 
        }
        );
       
        return patrolGroup.Where(x=>x.isFollower && x.isAttacking).All(x => Vector3.Distance(x.transform.position, x.agent.destination) < 1.1f);
    }
    void FindNearPoint()
    {
        var point = patrolList.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
        NextPatrolPoint = point;
    }
}
