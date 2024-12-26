using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public List<GameObject> enemyList;
   public TutorialCam tutorialCam;
    public bool EnemyCamMover;

    public float FirstWaveTime;
    public List<GameObject> enemyFirstList;
    public float SecondWaveTime;
    public List<GameObject> enemySecondList;
    public float ThirtWaveTime;
    public List<GameObject> enemyThirtList;
    public float FourthWaveTime;
    public List<GameObject> enemyFourthList;
    public float FifthWaveTime;
    public List<GameObject> enemyFifthList;
    public float SixthWaveTime;
    public List<GameObject> enemySixthList;

    private static Enemy _instance;
    public static Enemy Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {

        StartCoroutine(SpawnEnemy(enemyFirstList,FirstWaveTime));
        StartCoroutine(SpawnEnemy(enemySecondList, SecondWaveTime));
        StartCoroutine(SpawnEnemy(enemyThirtList, ThirtWaveTime));
        StartCoroutine(SpawnEnemy(enemyFourthList, FourthWaveTime));
        StartCoroutine(SpawnEnemy(enemyFifthList, FifthWaveTime));
        StartCoroutine(SpawnEnemy(enemySixthList, SixthWaveTime));
    }

    private IEnumerator SpawnEnemy(List<GameObject> enemyList, float WaveTime)
    {
        if (enemyList.Count == 0) yield break;
         enemyList.ForEach(x => x.SetActive(false));
        while (WaveTime>=0)
        {
            WaveTime -= Time.deltaTime;
            yield return null;
        }
        if (enemyList == enemyFirstList)
            tutorialCam.CameraMover(enemyList.First().transform, 3);
        enemyList.ForEach(x => x.SetActive(true));
    }
}
