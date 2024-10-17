using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] int crysterPerWatch;
    [SerializeField] float internetCheckInterval = 5f;
    WaitForSeconds internetCheckWait;
    bool adOpened;
    bool internetAvailable = false;

    void Start()
    {
        internetCheckWait = new WaitForSeconds(internetCheckInterval);

        StartCoroutine(CR_CheckInternet());
    }

    IEnumerator CR_CheckInternet()
    {
        do
        {
            if (IsConnectedToInternet() && !internetAvailable)
            {
                internetAvailable = true;

                button.interactable = true;
                AdManager.Instance.LoadRewardAd();
                RegisterAdEvents();
            }
            else if (!IsConnectedToInternet() && internetAvailable)
            {
                internetAvailable = false;
                button.interactable = false;
            }

            yield return internetCheckWait;
        } while (true);
    }

    bool IsConnectedToInternet()
    {
        // Check the internet reachability
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    void RegisterAdEvents()
    {
        AdManager.Instance.AddRewardAdEvent(AdEvent.AdOpened, () => OnRewardAdOpened());
        AdManager.Instance.AddRewardAdEvent(AdEvent.AdClosed, () => OnRewardAdClosed());
        AdManager.Instance.AddRewardAdEvent(AdEvent.AdClicked, () => OnRewardAdClicked());
    }

    public void OnClick()
    {
        AdManager.Instance.ShowRewardAd();
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

        // load ad for next time
        AdManager.Instance.LoadRewardAd();
        RegisterAdEvents();
    }

    void OnRewardAdClicked()
    {
        Debug.Log("player clicked ad");
    }
}