using System.Collections;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] AnimationKey[] animationKeys;
    [SerializeField] bool playOnEnable;
    [SerializeField] bool loop;
    Vector3 baseScale;

    void Awake()
    {
        baseScale = targetTrans.localScale;
    }

    void OnEnable()
    {
        if (playOnEnable)
        {
            StartCoroutine(CR_IdleSequence());
        }
    }

    IEnumerator CR_IdleSequence()
    {
        int i = 0, j = 1;
        do
        {
            yield return StartCoroutine(CR_ScaleAnimation(animationKeys[i].localScale, animationKeys[j].localScale, animationKeys[i].duration));

            if (i < animationKeys.Length - 1) i++;
            else i = 0;
            if (j < animationKeys.Length - 1) j++;
            else j = 0;
        } while (loop);
    }

    IEnumerator CR_ScaleAnimation(Vector3 _startValue, Vector3 _endValue, float _duration)
    {
        float tick = 0;
        while (tick < _duration)
        {
            tick += Time.deltaTime;
            targetTrans.localScale = Vector3.Lerp(_startValue, _endValue, tick / _duration);
            yield return null;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();

        targetTrans.localScale = baseScale;
    }
}
