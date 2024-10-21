using System;
using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] float speed;
    public float Speed
    {
        set { speed = value; }
        get { return speed; }
    }
    [SerializeField] float currentSpeed;
    public float CurrentSpeed
    {
        set { currentSpeed = value; }
        get { return currentSpeed; }
    }

    bool isMoving = false;
    Coroutine coroutine;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += currentSpeed * transform.forward * Time.deltaTime;
        }
    }

    public void ChangeSpeedOvertime(float _endValue, float _time)
    {
        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(CR_ChangeSpeed(_endValue, _time));
    }

    IEnumerator CR_ChangeSpeed(float _endValue, float _time)
    {
        float tick = 0f;
        float startValue = currentSpeed;
        while (!Mathf.Approximately(currentSpeed, _endValue))
        {
            tick += Time.deltaTime;
            currentSpeed = Mathf.Lerp(startValue, _endValue, tick / _time);
            yield return null;
        }
        currentSpeed = _endValue;
    }

    public void Move() => isMoving = true;

    public void Stop() => isMoving = false;

    public void Reset()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        Stop();
        currentSpeed = speed;
    }
}
