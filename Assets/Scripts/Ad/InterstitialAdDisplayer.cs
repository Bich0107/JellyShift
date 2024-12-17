using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAdDisplayer : MonoBehaviour
{
    [SerializeField] LoadPanel loadPanel;
    [Tooltip("Show an ad after player finish this number of level")]
    [SerializeField] int levelPerAd;
    int counter;

    public void UpdateCounter() => counter++;

#if UNITY_EDITOR || UNITY_ANDROID
    public void ShowInterstitialAd()
    {
        if (counter >= levelPerAd)
        {
            if (InternetHelper.s_InternetAvailable)
            {
                counter = 0;
                loadPanel.Open();
                AdManager.Instance.LoadInterstitialAd(() => loadPanel.Close());
            }
            else
            {
                Debug.LogWarning("Error: can't connect to the internet (interstital ad)");
            }
        }
    }
#else
    public void ShowInterstitialAd() { }
#endif
}
