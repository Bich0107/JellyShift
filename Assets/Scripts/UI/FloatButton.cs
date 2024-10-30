using System;
using System.Collections;
using UnityEngine;

public class FloatButton : MonoBehaviour
{
    [SerializeField] SoundHandler soundHandler;
    [SerializeField] Transform targetTrans;
    [SerializeField] GravityEffector gravity;
    [SerializeField] GameObject floatVFX;
    [SerializeField] SpriteToggleScript spriteToggleScript;
    [Space]
    [SerializeField] float floatHeight;
    [SerializeField] float moveTime;
    Vector3 pos;
    float baseHeight;
    bool isBusy;
    bool onGround = true;

    void Start()
    {
        baseHeight = targetTrans.localPosition.y;
    }

    public void Toggle()
    {
        if (isBusy) return;

        if (onGround)
        {
            MoveUp();
            onGround = false;
        }
        else
        {
            MoveDown();
            onGround = true;
        }
    }

    public void MoveUp()
    {
        soundHandler.Float();
        floatVFX.SetActive(true);

        gravity.enabled = false;

        StopAllCoroutines();
        StartCoroutine(CR_ChangeHeight(floatHeight));
    }

    public void MoveDown()
    {
        soundHandler.Descend();
        floatVFX.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(CR_ChangeHeight(baseHeight, () =>
        {
            gravity.enabled = true;
        }));
    }

    IEnumerator CR_ChangeHeight(float _endValue, Action _action = null)
    {
        isBusy = true;
        float tick = 0;

        float startHeight = targetTrans.localPosition.y;
        while (tick < moveTime)
        {
            tick += Time.deltaTime;

            pos = targetTrans.localPosition;
            pos.y = Mathf.Lerp(startHeight, _endValue, tick / moveTime);
            targetTrans.localPosition = pos;

            yield return null;
        }

        _action?.Invoke();
        isBusy = false;
    }

    public void Reset()
    {
        StopAllCoroutines();

        pos = targetTrans.localPosition;
        pos.y = baseHeight;
        targetTrans.localPosition = pos;

        floatVFX.SetActive(false);
        spriteToggleScript.Reset();

        isBusy = false;
        onGround = true;
    }
}
