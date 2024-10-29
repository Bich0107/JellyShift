using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSequence : MonoBehaviour
{
    [SerializeField] CustomAnimation[] animations;
    [SerializeField] UnityEvent playStartAction;
    [SerializeField] UnityEvent rewindEndAction;
    bool isBusy;
    bool rewindable = false;

    public void Play()
    {
        if (isBusy) return;
        isBusy = true;
        StartCoroutine(CR_PlaySequence());
    }

    public void Rewind()
    {
        if (isBusy || !rewindable) return;

        isBusy = true;
        StartCoroutine(CR_RewindSequence());
    }

    IEnumerator CR_PlaySequence()
    {
        playStartAction?.Invoke();

        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].Play();
            yield return animations[i].playWait;
        }

        isBusy = false;
        rewindable = true;
    }

    IEnumerator CR_RewindSequence()
    {
        for (int i = animations.Length - 1; i >= 0; i--)
        {
            animations[i].Rewind();
            yield return animations[i].rewindWait;
        }

        rewindEndAction?.Invoke();
        isBusy = false;

        rewindable = false;
    }
}
