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
    [SerializeField] Slider floatEnegyBar;
    [SerializeField] float restoreRate;
    [SerializeField] float floatHeight;
    [SerializeField] float floatDuration;
    [SerializeField] float moveTime;
    [SerializeField] float cooldown;
    WaitForSeconds cooldownWait;
    Vector3 pos;
    bool inCD;
    bool isFloating;
    float oldHeight;

    void Start()
    {
        cooldownWait = new WaitForSeconds(cooldown);
        floatEnegyBar.maxValue = floatDuration;
        floatEnegyBar.value = floatDuration;
    }

    void Update()
    {
        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        if (isFloating)
        {
            floatEnegyBar.value -= Time.deltaTime;
            if (floatEnegyBar.value <= 0f)
            {
                floatEnegyBar.value = 0f;
                isFloating = false;
                StartCoroutine(CR_ChangeHeight(oldHeight, () =>
                {
                    gravity.enabled = true;
                }));
                StartCoroutine(CR_Cooldown());
            }
        }
        else
        {
            floatEnegyBar.value += restoreRate * Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inCD) return;

        gravity.enabled = false;
        StopAllCoroutines();
        isFloating = true;

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

        float startHeight = targetTrans.position.y;
        while (tick < moveTime)
        {
            tick += Time.deltaTime;

            pos = targetTrans.position;
            pos.y = Mathf.Lerp(startHeight, _endValue, tick / moveTime);
            targetTrans.position = pos;

            yield return null;
        }

        _action?.Invoke();
    }

    public void Reset()
    {
        StopAllCoroutines();

        pos = targetTrans.position;
        pos.y = oldHeight;
        targetTrans.position = pos;

        floatEnegyBar.value = floatDuration;
        isFloating = false;
    }
}
