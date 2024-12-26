using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dreamteck.Splines;

public class MageController : MonoBehaviour
{
    [SerializeField] float Range;
    [SerializeField] int Damage;
    Animator animator;
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] Transform fireBallPos;
    [SerializeField] SplineComputer computer;
    [SerializeField] AudioSource audioSource;

    GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        controlRange();
    }

    private void controlRange()
    {

        if (target == null)
        {
            target = Enemy.Instance.enemyList.Where(x => x != null && Vector3.Distance(x.transform.position, transform.position) < Range && x.activeInHierarchy == true && !x.GetComponent<EnemyAI>().health.isDead).OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
            if (target != null)
                animator.SetBool("isAttacking", true);
            else
                animator.SetBool("isAttacking", false);
        }
        else
        {
            if (target.GetComponent<EnemyAI>().health.isDead)
                target = null;
            else
            {
                var pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                transform.LookAt(pos);
                computer.SetPointPosition(0, transform.position + Vector3.up * 1 + transform.forward * 1);
                computer.SetPointPosition(1, transform.position + Vector3.up * 1 + transform.forward * 2);
                computer.SetPointPosition(2, target.transform.position + Vector3.up * 1);
            }
        }
    }

    public void trowFireBall()
    {
        if (target == null) return;
        var x = Instantiate(fireBallPrefab, null);
        x.transform.position = fireBallPos.position;
        x.GetComponent<SplineFollower>().spline = computer;
        var fireBallController = x.GetComponent<FireBallController>();
        fireBallController.Damage = Damage;
        x = null;
        audioSource.PlayOneShot(GameSingleton.Instance.Sounds.Fireball);
    }
}
