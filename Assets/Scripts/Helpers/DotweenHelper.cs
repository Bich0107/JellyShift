using DG.Tweening;
using UnityEngine;

public class DotweenHelper : MonoSingleton<DotweenHelper>
{
    protected override void Awake()
    {
        base.Awake();
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultTimeScaleIndependent = true;
    }
}
