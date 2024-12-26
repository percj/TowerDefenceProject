using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadTrigger : MonoBehaviour
{
    VariableJoystick joystick;
    private void Start()
    {
        joystick = GetComponent<VariableJoystick>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var health = other.GetComponentInChildren<Health>();
            var animator = other.GetComponent<Animator>();

            health.ReSpawn();
            other.gameObject.transform.position = GameSingleton.Instance.levelManager.currLevelNeeds.StartPos.position;
            health.isDead = false;
            health.isDying = false;
            animator.SetBool("Dead", false);
            Sequence sequence2 = DOTween.Sequence();
            sequence2.AppendInterval(1);
            sequence2.OnComplete(() =>
            {
                joystick.enabled = true;
            });
        }
    }
}
