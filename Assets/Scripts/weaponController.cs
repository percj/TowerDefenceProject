using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class weaponController : MonoBehaviour
{
    [SerializeField] GameObject Sword;
    [SerializeField] DamageWithWeapon swordDamager;
    [SerializeField] float damage;


    [SerializeField] GameObject Pickaxe;
    [SerializeField] GameObject Axe;
    [SerializeField] Animator anim;
    bool isAttacking;
    Sequence sequence;
   
    public void HitWithSword()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("SwordAttack");
            isAttacking = true;
            sequence = DOTween.Sequence();
            sequence.Append(Sword.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce));
            sequence.OnComplete(() => FinishAttack(Sword));
        }
    }
    public void HitWithPickaxe()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("PickaxeAttack");
            isAttacking = true;
            sequence = DOTween.Sequence();
            sequence.Append(Pickaxe.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce));
            sequence.OnComplete(() => FinishAttack(Pickaxe));
        }
    }
    public void HitWithAxe()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("AxeAttack");
            isAttacking = true;
            sequence = DOTween.Sequence();
            sequence.Append(Axe.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce));
            sequence.OnComplete(() => FinishAttack(Axe));
        }
    }

    void FinishAttack(GameObject weapon)
    {
        isAttacking = false;
        weapon.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
    }

    public void damageWithSword()
    {
        swordDamager.damageWithSword(damage);
    }


}
