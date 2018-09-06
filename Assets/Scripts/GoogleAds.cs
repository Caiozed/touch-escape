using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class GoogleAds : MonoBehaviour
{
    // Use this for initialization
    BannerView bannerView;
    public void Start()
    {
        #if UNITY_ANDROID
                string appId = "ca-app-pub-7855200918488357~9058694966";
        #elif UNITY_IPHONE
                    string appId = "ca-app-pub-7855200918488357~9058694966";
        #else
                    string appId = "ca-app-pub-7855200918488357~9058694966";
        #endif

    
        Advertisement.Initialize("2780603", false);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
    }

    private void RequestBanner()
    {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7855200918488357/6703994362";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-7855200918488357/6703994362";
#else
            string adUnitId = "ca-app-pub-7855200918488357/6703994362";
#endif

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        bannerView.Show();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public static void ShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        if (Advertisement.IsReady())
        {
            Advertisement.Show(options);
        }
    }

    public static void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            LoadingManager.LoadLevel(LoadingManager.CurrentLevel() + 1);
            // Reward your player here.
        }
        else if (result == ShowResult.Skipped)
        {
            ShowRewardedVideo();
        }
        else if (result == ShowResult.Failed)
        {
            LoadingManager.LoadLevel(LoadingManager.CurrentLevel() + 1);
        }
    }

     public static void ShowRewardedVideoCurrent()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResultCurrent;

        Advertisement.Show("rewardedVideo", options);
    }

    public static void HandleShowResultCurrent(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            LoadingManager.LoadLevel(LoadingManager.CurrentLevel());
            // Reward your player here.
        }
        else if (result == ShowResult.Skipped)
        {
            ShowRewardedVideo();
        }
        else if (result == ShowResult.Failed)
        {
            LoadingManager.LoadLevel(LoadingManager.CurrentLevel());
        }
    }
}

