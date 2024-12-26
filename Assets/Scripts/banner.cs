using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class banner : MonoBehaviour
{
    private BannerView bannerView;
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
        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}