using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdButton : MonoBehaviour
{
    [SerializeField] LoadPanel loadPanel;
    [SerializeField] Button button;
    [SerializeField] int crystalPerWatch = 150;

#if UNITY_STANDALONE
    void Start()
    {
        button.interactable = false;
    }

    public void OnClick() {}
#else
    void Start()
    {
        button.interactable = true;
    }

    public void OnClick()
    {
        if (InternetHelper.s_InternetAvailable)
        {
            loadPanel.Open();
            AdManager.Instance.LoadRewardAd(() => OnRewardAdShowed());
        }
        else
        {
            Debug.LogWarning("Error: can't connect to the internet (reward ad)");
        }
    }

    void OnRewardAdShowed()
    {
        loadPanel.Close();
        Bank.Instance.AddCrystal(crystalPerWatch);
    }
#endif
}