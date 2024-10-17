using UnityEngine;

public class WatchAdButton : MonoBehaviour
{
    [SerializeField] int crysterPerWatch;

    public void OnClick()
    {
        AdManager.Instance.LoadRewardAd();
    }
}