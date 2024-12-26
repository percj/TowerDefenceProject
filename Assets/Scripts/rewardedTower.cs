using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class rewardedTower : MonoBehaviour
{
    //[SerializeField] GameObject error;
    [HideInInspector] public UnityEvent currEvent;
    private RewardedAd rewardedAd;
    [SerializeField] string AndroidID;
    [SerializeField] string IosID;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initstatus => { });
        RequestRewarded();
    }
    public void RequestRewarded()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = AndroidID;
#elif UNITY_IPHONE
            adUnitId = IosID;
#else
        adUnitId = "unexpected_platform";
#endif 
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        this.rewardedAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        currEvent.Invoke();
        RequestRewarded();
        currEvent.RemoveAllListeners();
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        //error.SetActive(true);
        RequestRewarded();
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //error.SetActive(true);
        RequestRewarded();
    }
}
