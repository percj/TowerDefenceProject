using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.EventSystems;

public class interstitial : MonoBehaviour
{
    [SerializeField] VariableJoystick joystick;
    private InterstitialAd interstitialAd;
    [SerializeField] float countdownValue;
    [SerializeField] string AndroidID;
    [SerializeField] string IosID;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initstatus => { });
        RequestInterstitial();
    }
    public void RequestInterstitial()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = AndroidID;
#elif UNITY_IPHONE
            adUnitId = IosID;
#else
        adUnitId = "unexpected_platform";
#endif 
        // Clean up interstitial before using
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        // Initialize an InterstitialAd.
        this.interstitialAd = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialAd.LoadAd(request);

        // Called when an ad is shown.
        this.interstitialAd.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
            interstitialAd.Show();
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        joystick.OnPointerUp(new PointerEventData(EventSystem.current));
        joystick.enabled = false;
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        joystick.enabled = true;
        RequestInterstitial();
        //StartCoroutine(RequestCountdown());
    }

    public void startCoroutine()
    {
        StartCoroutine(RequestCountdown());
    }

    public IEnumerator RequestCountdown()
    {
        var CountdownValue = countdownValue;
        while (CountdownValue > 0)
        {
            CountdownValue -= Time.deltaTime;
            yield return null;
        }
        ShowAd();
    }
}
