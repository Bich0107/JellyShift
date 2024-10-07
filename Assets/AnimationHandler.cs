using System.Collections;
using UnityEngine;

[System.Serializable]
public class AnimationKey
{
    public Vector3 offset;
    public Vector3 localScale;
    public float duration;
}

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationKey[] goalReachKeys;
    [SerializeField] AnimationKey[] idleKeys;
    [SerializeField] Transform scaleTrans;
    [SerializeField] float idleScaleDuration;
    [SerializeField] float goalReachScaleDuration;
    Vector3 baseScale;
    Vector3 goalReachScale;
    Coroutine coroutine;

    void Awake()
    {
        animator = GetComponent<Animator>();

        animator.enabled = false;
        baseScale = scaleTrans.localScale;
    }

    void Start()
    {
        coroutine = StartCoroutine(CR_IdleSequence());
    }

    public void TurnOff()
    {
        animator.enabled = false;
        scaleTrans.localScale = baseScale;
        StopCoroutine(coroutine);
    }

    public void GoalReach()
    {
        animator.enabled = true;
        GoalReachAnimation();
    }

    void GoalReachAnimation()
    {
        StartCoroutine(CR_GoalReachSequence());
    }

    IEnumerator CR_IdleSequence()
    {
        do
        {
            yield return StartCoroutine(CR_ScaleAnimation(idleKeys[0].localScale, idleKeys[1].localScale, idleScaleDuration));
            yield return StartCoroutine(CR_ScaleAnimation(idleKeys[1].localScale, idleKeys[0].localScale, idleScaleDuration));
        } while (true);
    }

    IEnumerator CR_GoalReachSequence()
    {
        StartCoroutine(CR_ScaleAnimation(scaleTrans.localScale, baseScale, goalReachScaleDuration));
        for (int i = 0; i < goalReachKeys.Length; i++)
        {
            yield return StartCoroutine(CR_MoveAnimation(goalReachKeys[i]));
        }
        StartCoroutine(CR_IdleSequence());
    }

    IEnumerator CR_MoveAnimation(AnimationKey _animationKey)
    {
        float tick = 0;
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = startPosition + transform.localRotation * _animationKey.offset;
        while (tick < _animationKey.duration)
        {
            tick += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, tick / _animationKey.duration);
            yield return null;
        }
    }

    IEnumerator CR_ScaleAnimation(Vector3 _startValue, Vector3 _endValue, float _duration)
    {
        float tick = 0;
        while (tick < _duration)
        {
            tick += Time.deltaTime;
            scaleTrans.localScale = Vector3.Lerp(_startValue, _endValue, tick / _duration);
            yield return null;
        }
    }

}
