using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    [HideInInspector] public StationController selectedStation;
    [HideInInspector] public Transform exitPos;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    bool willExit;
    bool finallyStateStart = false;
    bool inStation = false;


    [Header("=== Frustrade Logic ===")]
    [SerializeField] bool canFrustrurate;
    [SerializeField] GameObject frustrateCanvas;
    [SerializeField] Image frustrate;
    [SerializeField] GameObject frustrateAngry;
    [Range(0,20)][SerializeField] float frustrateElapsed;
    bool isFrustrurate;
    float frustrateTimer;

    void Start()
    {
        agent.SetDestination(selectedStation.intract.position);
    }

    // Update is called once per frame
    void Update()
    {
        AnimationControl();

        if (!finallyStateStart && inStation)
        {
            StartCoroutine(FinalyState());
        }
    }

    IEnumerator FinalyState()
    {
        finallyStateStart = true;
        if (canFrustrurate)
        {
            frustrateCanvas.SetActive(true);

            while (frustrateTimer < frustrateElapsed)
            {
                frustrateTimer += Time.deltaTime;
                frustrate.fillAmount = frustrateTimer / frustrateElapsed;
                if (selectedStation.stationObjects.Count > 0)
                {
                    animator.SetTrigger("Succeed");
                    selectedStation.removeObject();
                    frustrateCanvas.SetActive(false);
                    isFrustrurate = false;
                    yield return new WaitForSeconds(2);
                    selectedStation.moneyAreaController.addMoney();
                    break;
                }
                yield return null;

            }
            frustrateAngry.SetActive(true);


        }
        else
        {
            while (selectedStation.stationObjects.Count <= 0)
            {
                     yield return null;
            }
            animator.SetTrigger("Succeed");
            selectedStation.removeObject();
            yield return new WaitForSeconds(2);

        }

        agent.SetDestination(exitPos.position);
        willExit = true;
        selectedStation.currCustomer = null;
        yield return null;
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
            if(Vector3.Distance(agent.destination, selectedStation.intract.position)< 0.2f)
                inStation = true;
            animator.SetBool("run", false);
            agent.isStopped = true;
            if (willExit)
            {
                Destroy(gameObject);
                return;
            }

        }
    }
}
