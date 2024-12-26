using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class WallHealth : Health
{
   Vector3 bigScale;
   Vector3 smallScale;


    public override void decreaseHealth(float damage)
    {
        base.decreaseHealth(damage);
        if (CurrentHealth <= 0)
        {
            Invoke("tryGoTown", 0.1f);
            
        }
        hitAnimation();
    }

    public void tryGoTown()
    {
         Enemy.Instance.enemyList.ForEach(x => { if(x.activeInHierarchy) x.GetComponent<EnemyAI>().tryToGoTown(); });
        destroyObject.SetActive(false);
    }
    
    void hitAnimation()
    {
       
        if (delayTime <= delay)
        {
            bigScale = animationTransform.localScale + (Vector3.one * 0.1f);
            smallScale = animationTransform.localScale;
            animationTransform.DOScale(bigScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                animationTransform.DOScale(smallScale, 0.5f).SetEase(Ease.OutBounce);
            });
        }
        delay = 0;
    }
}
