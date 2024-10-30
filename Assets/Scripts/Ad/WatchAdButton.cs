using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdButton : MonoBehaviour
{
    [SerializeField] LoadPanel loadPanel;
    [SerializeField] Button button;
    [SerializeField] int crysterPerWatch;

#if UNITY_STANDALONE
    public void OnClick()
    {
    }
#else
    public void OnClick()
    {
        if (InternetHelper.s_InternetAvailable)
        {
            loadPanel.Open();
            AdManager.Instance.LoadRewardAd(() => OnRewardAdClosed());
        }
        else
        {
            Debug.LogWarning("Error: can't connect to the internet (reward ad)");
        }
    }

    void OnRewardAdClosed()
    {
        loadPanel.Close();
        Bank.Instance.AddCrystal(crysterPerWatch);
    }
#endif
}