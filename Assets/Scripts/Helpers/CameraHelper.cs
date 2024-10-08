using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    Camera mainCam;
    float fieldOfView => mainCam.fieldOfView;
    Coroutine coroutine;

    void Awake()
    {
        mainCam = Camera.main;
    }

    public void ChangeFOVOverTime(float _endValue, float _time)
    {
        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(CR_ChangeFOV(_endValue, _time));
    }

    IEnumerator CR_ChangeFOV(float _endValue, float _time)
    {
        float tick = 0f;
        float startValue = fieldOfView;
        while (!Mathf.Approximately(fieldOfView, _endValue))
        {
            tick += Time.deltaTime;
            mainCam.fieldOfView = Mathf.Lerp(startValue, _endValue, tick / _time);
            yield return null;
        }

        mainCam.fieldOfView = _endValue;
    }
}
