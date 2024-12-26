using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;



public class NextMaterialLevel : MonoBehaviour
{
    float nextLevelTimer;
    [SerializeField] float nextLevelTime;
    [SerializeField] GameObject nextLevelObject;
    bool isStart;



    private void OnDisable()
    {
        nextLevelTimer = nextLevelTime;
        isStart = false;
    }
    private void OnEnable()
    {
        nextLevelTimer = nextLevelTime;
        isStart = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (nextLevelTimer > 0)
        {
            if (!isStart) yield break;
            nextLevelTimer -= Time.deltaTime;
            yield return null;
        }

        nextLevelObject.SetActive(true);
        nextLevelObject.transform.DOShakeScale(1, 0.2f, 5);
        gameObject.SetActive(false);
    }
}