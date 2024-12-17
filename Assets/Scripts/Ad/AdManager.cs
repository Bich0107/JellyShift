using System;
using GoogleMobileAds.Api;
using UnityEngine;

public enum AdEvent
{
    AdOpened,
    AdClosed,
}

public class AdManager : MonoSingleton<AdManager>
{
    public string app_id = "ca-app-pub-2136479507730706~2612843247";

#if UNITY_EDITOR // for testing in editor
    string rewardId = "ca-app-pub-3940256099942544/5224354917";
    string interstitialId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
    string rewardId = "ca-app-pub-2136479507730706/6183718326";
    string interstitialId = "ca-app-pub-2136479507730706/2458728759";
#else // for other platform
    string rewardId = "";
    string interstitialId = "";
#endif

    RewardedAd rewardedAd;
    InterstitialAd interstitialAd;

#if UNITY_EDITOR || UNITY_ANDROID
    void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => { });
    }
#endif

    #region Reward ad
    public void LoadRewardAd(Action _action = null)
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(rewardId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                _action?.Invoke();
                rewardedAd = ad;
                ShowRewardAd();
            });
    }

    public void AddRewardAdEvent(AdEvent _eventType, Action _action)
    {
        switch (_eventType)
        {
            case AdEvent.AdOpened:
                rewardedAd.OnAdFullScreenContentOpened += _action;
                break;
            case AdEvent.AdClosed:
                rewardedAd.OnAdFullScreenContentClosed += _action;
                break;
        }
    }

    public void ShowRewardAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) => { });
        }
    }
    #endregion

    #region Interstitial ad
    public void LoadInterstitialAd(Action _action = null)
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(interstitialId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _action?.Invoke();
                interstitialAd = ad;
                ShowInterstitialAd();
            });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }
    #endregion
}
