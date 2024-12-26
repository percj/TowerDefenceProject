using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DamageAnimation : MonoBehaviour
{
    public float damage;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] Image damageTypeImage;
    private void Start()
    {
        damageText.text = damage.ToString();
        Sequence sequence = DOTween.Sequence();
        Vector3 move = Vector3.up * Random.Range(1,2);
        move += Vector3.left * Random.Range(-1, 1);
        sequence.Append(transform.DOMove(transform.position+ move, 0.5f));
        sequence.Join(transform.DOScale(Vector3.one * 1.8f, 0.5f));
        sequence.Append(transform.DOMove(transform.position , 0.5f));
        sequence.Join(transform.DOScale(0, 0.5f));
        sequence.OnComplete(() => { Destroy(gameObject); });
    }
}
