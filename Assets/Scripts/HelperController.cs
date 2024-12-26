using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HelperController : MonoBehaviour
{
    [SerializeField] List<float> speedLevelAmount;
    [SerializeField] List<float> collectSpeedLevelAmount;
    [SerializeField] List<int> capacityLevelAmount;
    [SerializeField] NavMeshAgent agent;
    public Transform Collect;
    public Transform WaitPos;
    [SerializeField] carryObjects carryObjects;
    [SerializeField] Animator animator;

    public CustomerSpawner customerSpawner;

    float collectTimer;
    [Range(0, 5f)][SerializeField] float collectElapsed;
    float refillTimer;
    [Range(0, 5f)][SerializeField] float refillElapsed;
    StationController selectedStation;

    public int collectSpeedLevel;
    public int speedLevel;
    public int capacityLevel;

    void Start()
    {
        agent.SetDestination(Collect.position);
        setSpeedLevel(speedLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedStation == null) findStation();
        AnimationControl();
    }
    private void AnimationControl()
    {
        if (Vector3.Distance(agent.destination, transform.position) > 0.2f)
        {
            animator.SetBool("run", true);
            agent.isStopped = false;
        }
        else
        {
            animator.SetBool("run", false);
            agent.isStopped = true;
        }

        animator.SetBool("Carry", carryObjects.currCount>0);
        
    }

    void findStation()
    {
        if(carryObjects.currCount != 0)
        {
            var selectedStations = customerSpawner.stations.Where(x => x.unlockedManager.isUnlocked == true).OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            if (selectedStations.Count == 0)
            {
                selectedStation = null;
                agent.SetDestination(WaitPos.position);
            }
            else
            {
                selectedStation = selectedStations[0];
                agent.SetDestination(selectedStations[0].stationObjectPos.position);
            }
        }
        else agent.SetDestination(Collect.position);
    }

    private void OnTriggerStay(Collider other)  
    {
        if (other.gameObject.tag == "Collect")
        {
            if(carryObjects.carryLimit > carryObjects.currCount)
            {
                collectTimer += Time.deltaTime;
                if (collectTimer > collectElapsed)
                {
                    collectTimer = 0;
                    var carryStation = other.GetComponent<CollectStation>();
                    if (carryStation.removeCarryObject())
                        carryObjects.addObject();
                }
            }
            else
            {
                findStation();
            }
        }

        if (other.gameObject.tag == "StationObjects")
        {
            var intractStation = other.gameObject.transform.parent.GetComponent<StationController>();
            if (intractStation.unlockedManager.isUnlocked && 0 < carryObjects.currCount)
            {
                refillTimer += Time.deltaTime;
                if (refillTimer > refillElapsed)
                {
                    refillTimer = 0;
                    intractStation.addObject();
                    carryObjects.removeObject();
                }
            }
            if (carryObjects.currCount == 0) agent.SetDestination(Collect.position);

        }
    }

    internal void setCapacityLevel(int capacityLevel)
    {
        this.capacityLevel = capacityLevel;
        carryObjects.carryLimit = capacityLevelAmount[--capacityLevel];
    }

    internal void setCollectSpeedLevel(int collectSpeedLevel)
    {
        this.collectSpeedLevel = collectSpeedLevel;
        refillElapsed = collectSpeedLevelAmount[--collectSpeedLevel];
        collectElapsed = collectSpeedLevelAmount[collectSpeedLevel];
    }

    internal void setSpeedLevel(int speedLevel)
    {
        this.speedLevel = speedLevel;
        agent.speed = speedLevelAmount[--speedLevel];
    }
}
