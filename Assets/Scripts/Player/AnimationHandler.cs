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
    [SerializeField] Transform targetTrans;
    [SerializeField] float idleScaleDuration;
    [SerializeField] float goalReachScaleDuration;
    [SerializeField] string animationState_GoalReach = "Player_Goal";
    Vector3 baseScale;
    Quaternion baseRotation;

    void Awake()
    {
        animator = GetComponent<Animator>();

        animator.enabled = false;
        baseScale = targetTrans.localScale;
        baseRotation = targetTrans.localRotation;
    }

    void Start()
    {
        StartCoroutine(CR_IdleSequence());
    }

    public void StartIdleSequence() => StartCoroutine(CR_IdleSequence());

    public void GoalReach()
    {
        animator.enabled = true;
        animator.Play(animationState_GoalReach, 0, 0f);
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
        StartCoroutine(CR_ScaleAnimation(targetTrans.localScale, baseScale, goalReachScaleDuration));
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
            targetTrans.localScale = Vector3.Lerp(_startValue, _endValue, tick / _duration);
            yield return null;
        }
    }

    public void Reset()
    {
        animator.enabled = false;

        targetTrans.localScale = baseScale;
        targetTrans.localRotation = baseRotation;
        StopAllCoroutines();
    }
}
