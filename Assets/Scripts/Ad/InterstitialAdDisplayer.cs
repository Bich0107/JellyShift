using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAdDisplayer : MonoBehaviour
{
    [Tooltip("Show an ad after player finish this number of level")]
    [SerializeField] int levelPerAd;
    int counter;

    public void UpdateCounter() => counter++;
#if UNITY_ANDROID || UNITY_EDITOR
    public void ShowInterstitialAd()
    {
        if (counter >= levelPerAd)
        {
            if (InternetHelper.s_InternetAvailable)
            {
                AdManager.Instance.LoadInterstitialAd();
                AdManager.Instance.ShowInterstitialAd();
                counter = 0;
            }
            else
            {
                Debug.LogWarning("Error: can't connect to the internet (interstital ad)");
            }
        }
    }
#else
    public void ShowInterstitialAd(){

    }
#endif
}
