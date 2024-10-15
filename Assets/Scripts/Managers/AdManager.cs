using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoSingleton<AdManager>
{
    static readonly string APP_KEY = "1fd85807d";
    [SerializeField] bool testMode;
    bool available;

    protected override void Awake()
    {
        base.Awake();

        IronSourceAdQuality.Initialize(APP_KEY);

        IronSource.Agent.init(APP_KEY, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.validateIntegration();

        ISAdQualityConfig adQualityConfig = new ISAdQualityConfig();
        adQualityConfig.TestMode = testMode;
        //IronSource.Agent.shouldTrackNetworkState(true);
    }

    void Start()
    {

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;

        IronSource.Agent.loadRewardedVideo();
    }

    /************* RewardedVideo AdInfo Delegates *************/
    // Indicates that there’s an available ad.
    // The adInfo object includes information about the ad that was loaded successfully
    // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
    }

    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
    }

    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
    }

    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
    }

    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
    }

    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {

    }

    public void ShowAd()
    {
        available = IronSource.Agent.isRewardedVideoAvailable();

        if (available)
        {
            IronSource.Agent.showRewardedVideo();
            Debug.Log("ad showed");
        }
        else
        {
            Debug.Log("ad unavailable");
        }
    }
}
