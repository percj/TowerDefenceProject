using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [HideInInspector] public float delay;
    public float delayTime;
    [SerializeField] float healthBarDelayTime;
    [HideInInspector] public float CurrentHealth;
    public DestroyableObject destroyableObject;
    [SerializeField] Image healthBarRed;
    [SerializeField] Image healthBarOrange;
    public Transform animationTransform;
    public GameObject destroyObject;
    public GameObject healtBar;
    [SerializeField] DamageAnimation damageAnimation;
    public bool hasHitAnim;
    public bool isDying;
    public bool isDead;
    void Start()
    {
        CurrentHealth = destroyableObject.MaxHealth;
        delay = delayTime;
    }
    public void ReSpawn()
    {
        CurrentHealth = destroyableObject.MaxHealth;
        delay = delayTime;
    }
    void  Update()
    { 
        if(!isDead)
        {
            delay += Time.deltaTime;
            if (healthBarDelayTime >= delay) healtBar.SetActive(true);
            else healtBar.SetActive(false);
        }
    }
  
    public virtual void decreaseHealth(float damage)
    {
        if (isDead) return;
        var x =Instantiate(damageAnimation, null);
        x.damage = damage;
        x.transform.SetPositionAndRotation(transform.position+(Vector3.up*2), new Quaternion());
        CurrentHealth -= damage;
        healthBarRed.fillAmount = CurrentHealth / destroyableObject.MaxHealth;
        if (CurrentHealth <= 0)
        {
            isDead = true;
        }
        delay = 0;
        
    }
}
