using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform targetTrans;
    [SerializeField] GravityEffector gravity;
    [SerializeField] float floatHeight;
    [SerializeField] float moveTime;
    Vector3 pos;
    bool isBusy;
    bool isFloating;
    float oldHeight;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFloating || isBusy) return;

        gravity.enabled = false;
        StopAllCoroutines();
        isFloating = true;

        pos = targetTrans.localPosition;
        oldHeight = pos.y;
        StartCoroutine(CR_ChangeHeight(floatHeight));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isFloating || isBusy) return;

        StopAllCoroutines();

        StartCoroutine(CR_ChangeHeight(oldHeight, () =>
        {
            gravity.enabled = true;
            isFloating = false;
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
        pos.y = oldHeight;
        targetTrans.localPosition = pos;

        isBusy = false;
        isFloating = false;
    }
}
