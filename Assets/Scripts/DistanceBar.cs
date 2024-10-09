using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DistanceBar : MonoBehaviour
{
    [SerializeField] Slider bar;
    [SerializeField] MovingObject movingObject;
    [SerializeField] float speedRatio = 0.47f;

    void Update()
    {
        if (GameManager.Instance.gameStarted) bar.value += movingObject.CurrentSpeed * speedRatio * Time.deltaTime;
    }

    public void SetMaxDistance(float _value)
    {
        bar.maxValue = _value;
    }

    public void SetProgressDistance(float _value)
    {
        bar.value = _value;
    }

    public void Reset()
    {
        bar.value = 0f;
    }
}
