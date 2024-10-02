using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Camera cam;
    [SerializeField] MovingObject movingObject;
    [SerializeField] ObjectScaler objectScaler;
    [SerializeField] float getTouchInterval = 0.1f;
    [SerializeField] bool isActive = true;
    WaitForSeconds getTouchWait;
    Vector3 touchPosition;
    float touchHeight;
    float distanceFromTouchHeight;

    void Start()
    {
        cam = Camera.main;
        getTouchWait = new WaitForSeconds(getTouchInterval);
        StartCoroutine(CR_GetTouchPos());
    }

    void Update()
    {
        if (isActive) HandleTouch();
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    movingObject.Move();
                    touch = Input.GetTouch(0);
                    TouchPosToWorldSpace(touch.position);
                    touchHeight = cam.ScreenToWorldPoint(touchPosition).y;
                    break;
                case TouchPhase.Moved:
                    TouchPosToWorldSpace(touch.position);
                    distanceFromTouchHeight = cam.ScreenToWorldPoint(touchPosition).y - touchHeight;
                    objectScaler.Scale(distanceFromTouchHeight);
                    break;
            }
        }
    }

    IEnumerator CR_GetTouchPos()
    {
        Touch touch;
        do
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                TouchPosToWorldSpace(touch.position);
                touchHeight = cam.ScreenToWorldPoint(touchPosition).y;
            }
            yield return getTouchWait;
        } while (true);
    }

    void TouchPosToWorldSpace(Vector2 _touchPos)
    {
        touchPosition = _touchPos;
        touchPosition.z = Camera.main.nearClipPlane;
    }

    public void SetActive(bool _value) => isActive = _value;
}
