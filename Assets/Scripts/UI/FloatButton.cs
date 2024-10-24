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
    [SerializeField] float cooldown;
    WaitForSeconds cooldownWait;
    Vector3 pos;
    bool inCD;
    bool isFloating;
    [SerializeField] float oldHeight;

    void Start()
    {
        cooldownWait = new WaitForSeconds(cooldown);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inCD) return;

        gravity.enabled = false;
        StopAllCoroutines();
        isFloating = true;

        pos = targetTrans.localPosition;
        oldHeight = pos.y;
        StartCoroutine(CR_ChangeHeight(floatHeight));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isFloating) return;

        StopAllCoroutines();
        isFloating = false;

        StartCoroutine(CR_ChangeHeight(oldHeight, () =>
        {
            gravity.enabled = true;
            StartCoroutine(CR_Cooldown());
        }));
    }

    IEnumerator CR_Cooldown()
    {
        inCD = true;
        yield return cooldownWait;
        inCD = false;
    }

    IEnumerator CR_ChangeHeight(float _endValue, Action _action = null)
    {
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
    }

    public void Reset()
    {
        StopAllCoroutines();

        pos = targetTrans.localPosition;
        pos.y = oldHeight;
        targetTrans.localPosition = pos;

        isFloating = false;
    }
}
