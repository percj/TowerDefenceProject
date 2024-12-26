using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour
{
    Vector3 bigScale;
    Vector3 smallScale;
    float delay;
    [SerializeField] float delayTime;
    [SerializeField] float healthBarDelayTime;
    [HideInInspector] public float CurrentHealth;
    [SerializeField] DestroyableObject destroyableObject;
    [SerializeField] Image healthBarRed;
    [SerializeField] Image healthBarOrange;
    [SerializeField] Transform animationTransform;
    [SerializeField] GameObject destroyObject;
    [SerializeField] GameObject healtBar;
    [SerializeField] DamageAnimation damageAnimation;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = destroyableObject.MaxHealth;
        delay = delayTime;
        bigScale = animationTransform.localScale + (Vector3.one * 0.1f);
        smallScale = animationTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            decreaseHealth(10);
            hitAnimation();
        }
        delay += Time.deltaTime;
        if (healthBarDelayTime >= delay) healtBar.SetActive(true);
        else healtBar.SetActive(false);
    }

    public void decreaseHealth(float damage)
    {
        var x = Instantiate(damageAnimation, null);
        x.damage = damage;
        x.transform.SetPositionAndRotation(transform.position + (Vector3.up * 2), new Quaternion());
        CurrentHealth -= damage;
        healthBarRed.fillAmount = CurrentHealth / destroyableObject.MaxHealth;
        hitAnimation();
        if (CurrentHealth <= 0)
        {
            if (Castle.Instance.destroyableObjects.Contains(destroyObject))
            {
                Castle.Instance.destroyableObjects.Remove(destroyObject);
            }
            if (Enemy.Instance.enemyList.Contains(destroyObject)) Castle.Instance.destroyableObjects.Remove(destroyObject);
            Destroy(destroyObject);
            Enemy.Instance.enemyList.ForEach(x => x.GetComponent<EnemyAI>().tryToGoTown());
        }
    }

    void hitAnimation()
    {
        if (delayTime <= delay)
        {
            animationTransform.DOScale(bigScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                animationTransform.DOScale(smallScale, 0.5f).SetEase(Ease.OutBounce);
            });
        }
        delay = 0;
    }
}

