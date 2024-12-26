using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using GoogleMobileAds.Api;


public class levelManager : MonoBehaviour
{
    [SerializeField] string NextLevel;
    [SerializeField] string CurrLevel;

    [SerializeField] GameObject fail;
    [SerializeField] GameObject success;
    [SerializeField] starTimer starTimer;
    [SerializeField] Image SuccesFiller;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI currLevelName;
    [SerializeField] interstitial interstitialAd;
    [SerializeField] LoadingScreenManager loading;
    [HideInInspector]public levelsNeeds currLevelNeeds;

    public UnityEvent playEvent;

    private void Awake()
    {
        LoadData(); 
        playEvent.Invoke();
        starTimer.startTimer = true;
        starTimer.time = 0;
        Time.timeScale = 1;
    }

    private void LoadData()
    {
        currLevelName.text = CurrLevel;
        currLevelNeeds = FindObjectOfType<levelsNeeds>();
    }

    public void failed()
    {
        interstitialAd.startCoroutine(); 
        GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Lose);
        
        starTimer.startTimer = false;
        fail.SetActive(true);
    }
    public void finished()
    {      
        interstitialAd.startCoroutine(); 
        Invoke("WinScreen", 2);
    }
    void WinScreen()
    {
        starTimer.startTimer = false;
        success.SetActive(true);
        giveStar();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        loading.LoadScreen("MainMenu");
    }

    public void giveStar()
    {
        if (starTimer.time < currLevelNeeds.threeStarSecond)
        {
            StartCoroutine(giveStar(3));
            var currStar = PlayerPrefs.GetInt(CurrLevel+ "starCount", 3);
            if(currStar <= 3)
                PlayerPrefs.SetInt(CurrLevel + "starCount", 3);
        }
        else if (starTimer.time < currLevelNeeds.twoStarSecond)
        {
            StartCoroutine(giveStar(2));

            var currStar = PlayerPrefs.GetInt(CurrLevel + "starCount", 2);
            if (currStar <= 2)
                PlayerPrefs.SetInt(CurrLevel + "starCount", 2);
        }
        else if (starTimer.time < currLevelNeeds.oneStarSecond)
        {
            StartCoroutine(giveStar(1));

            var currStar = PlayerPrefs.GetInt(CurrLevel + "starCount", 1);
            if (currStar <= 1)
                PlayerPrefs.SetInt(CurrLevel + "starCount", 1);
        }

        PlayerPrefs.SetString(NextLevel + "isLock", "True");
        PlayerPrefs.SetString("LastLevelName", NextLevel);
        PlayerPrefs.Save();
    }

    public IEnumerator giveStar(int starCount)
    {
        GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Win);
        float BarRandNumer = 0;
        Sequence sequence = DOTween.Sequence();
        if (starCount > 0) { sequence.Append(starTimer.firstStar.transform.DOScale(1, 0.3f).OnComplete(()=> GameSingleton.Instance.Sounds.PlayOneShot( GameSingleton.Instance.Sounds.Gain,0.5f))); BarRandNumer += UnityEngine.Random.Range(0.20f, 0.33f); }
        if(starCount > 1)   {sequence.Append(starTimer.secondStar.transform.DOScale(1, 0.3f).OnComplete(() => GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Gain, 0.5f))); BarRandNumer += UnityEngine.Random.Range(0.20f, 0.33f); }
        if (starCount > 2) { sequence.Append(starTimer.thirdStar.transform.DOScale(1, 0.3f).OnComplete(() => GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Gain, 0.5f))); BarRandNumer += UnityEngine.Random.Range(0.20f, 0.33f); }
        sequence.OnComplete(()=>sequence.Kill());
        if (BarRandNumer == 0) BarRandNumer = UnityEngine.Random.Range(0.05f, 0.15f);
        float timer = 0f;
        int coin = 0;
        while (timer <= BarRandNumer)
        {
            coin += UnityEngine.Random.Range(1, 3);
            coinText.text = "+" + coin.ToString();
                GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Money,0.1f);
            SuccesFiller.fillAmount = timer;
            timer += Time.deltaTime;
            yield return null;
        }
        var totalCoin =PlayerPrefs.GetInt("TotalEarnedCoin", 0);
        PlayerPrefs.SetInt("TotalEarnedCoin", totalCoin + coin); 
    }

    public void nextLevel()
    {
        var lastLevelName = PlayerPrefs.GetString("LastLevelName", "Level 1");
        PlayerPrefs.SetString("CurrOpenningLevel", lastLevelName);
        loading.LoadScreen(NextLevel);
    }

    public void retryLevel()
    {
        loading.LoadScreen(CurrLevel);
    }
}
