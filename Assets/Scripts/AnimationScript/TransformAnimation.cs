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

public class TransformAnimation : CustomAnimation
{
    AnimationFrame baseFrame = new AnimationFrame();
    [SerializeField] RectTransform rectTrans;
    [SerializeField] AnimationFrame startFrame;
    [SerializeField] AnimationFrame endFrame;
    [SerializeField] bool useUnscaleTime = true;
    Sequence playSequence;
    Sequence rewindSequence;

    protected override void Start()
    {
        base.Start();

        baseFrame.position = rectTrans.localPosition;
        baseFrame.scale = rectTrans.localScale;

        playSequence = DOTween.Sequence();
        playSequence.Join(rectTrans.DOLocalMove(endFrame.position, endFrame.time));
        playSequence.Join(rectTrans.DOScale(endFrame.scale, endFrame.time));
        playSequence.SetAutoKill(false).SetUpdate(useUnscaleTime);

        rewindSequence = DOTween.Sequence();
        rewindSequence.Join(rectTrans.DOLocalMove(startFrame.position, startFrame.time));
        rewindSequence.Join(rectTrans.DOScale(startFrame.scale, startFrame.time));
        rewindSequence.SetAutoKill(false).SetUpdate(useUnscaleTime);
    }

    public override void Play()
    {
        playSequence.Restart();
        playSequence.Play();
    }

    public override void Rewind()
    {
        rewindSequence.Restart();
        rewindSequence.Play();
    }

    public void Reset()
    {
        rectTrans.localScale = baseFrame.scale;
        rectTrans.localPosition = baseFrame.position;
    }
}
