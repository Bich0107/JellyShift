using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] int crysterPerWatch;
    bool adOpened;

    void RegisterAdEvents()
    {
        AdManager.Instance.AddRewardAdEvent(AdEvent.AdOpened, () => OnRewardAdOpened());
        AdManager.Instance.AddRewardAdEvent(AdEvent.AdClosed, () => OnRewardAdClosed());
    }

    public void OnClick()
    {
        if (InternetHelper.s_InternetAvailable)
        {
            AdManager.Instance.LoadRewardAd();
            RegisterAdEvents();
            AdManager.Instance.ShowRewardAd();
        }
        else
        {
            Debug.LogWarning("Error: can't connect to the internet (reward ad)");
        }
    }

    void OnRewardAdOpened()
    {
        adOpened = true;
    }

    void OnRewardAdClosed()
    {
        if (!adOpened) return;

        adOpened = false;
        Bank.Instance.AddCrystal(crysterPerWatch);
    }
}