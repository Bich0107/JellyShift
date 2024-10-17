using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAdDisplayer : MonoBehaviour
{
    [Tooltip("Show an ad after player finish this number of level")]
    [SerializeField] int levelPerAd;
    int counter;

    public void UpdateCounter()
    {
        counter++;
        if (counter >= levelPerAd)
        {
            ShowInterstitialAd();
            counter = 0;
        }
    }

    void ShowInterstitialAd()
    {
        if (InternetHelper.s_InternetAvailable)
        {
            AdManager.Instance.LoadInterstitialAd();
            AdManager.Instance.ShowInterstitialAd();
        }
        else
        {
            Debug.LogWarning("Error: can't connect to the internet (interstital ad)");
        }

    }
}
