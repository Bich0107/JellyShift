using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[Serializable]
public class AnimationFrame
{
    public Vector3 position;
    public Vector3 scale;
    public float time;
}

public class WindowAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTrans;
    [SerializeField] AnimationFrame startFrame;
    [SerializeField] AnimationFrame endFrame;
    [SerializeField] UnityEvent onAnimationEnd;
    Sequence playSequence;
    Sequence rewindSequence;

    void Start()
    {
        playSequence = DOTween.Sequence();
        playSequence.Join(rectTrans.DOLocalMove(endFrame.position, endFrame.time));
        playSequence.Join(rectTrans.DOScale(endFrame.scale, endFrame.time));
        if (onAnimationEnd != null)
            playSequence.OnComplete(() => onAnimationEnd.Invoke());
        playSequence.SetAutoKill(false);

        rewindSequence = DOTween.Sequence();
        rewindSequence.Join(rectTrans.DOLocalMove(startFrame.position, startFrame.time));
        rewindSequence.Join(rectTrans.DOScale(startFrame.scale, startFrame.time));
        if (onAnimationEnd != null)
            rewindSequence.OnComplete(() => onAnimationEnd.Invoke());
        rewindSequence.SetAutoKill(false);
    }

    public void Play()
    {
        playSequence.Restart();
        playSequence.Play();
    }

    public void Rewind()
    {
        rewindSequence.Restart();
        rewindSequence.Play();
    }
}
