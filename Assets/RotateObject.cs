using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 rotateSpeed;
    [SerializeField] bool playOnEnable;
    bool isRotating = false;

    void OnEnable()
    {
        if (playOnEnable) isRotating = true;
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            transform.Rotate(rotateSpeed * Time.fixedDeltaTime);
        }
    }

    public void Rotate() => isRotating = true;
    public void Stop() => isRotating = false;
}
