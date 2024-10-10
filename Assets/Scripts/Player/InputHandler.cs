using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Camera cam;
    [SerializeField] GravityEffector gravityEffector;
    [SerializeField] ObjectScaler objectScaler;
    [SerializeField] float getTouchInterval = 0.1f;
    [SerializeField] bool isActive = true;
    WaitForSeconds getTouchWait;
    Vector3 touchPosition;
    float touchHeight;
    float yDistanceFromTouchHeight;

    void Start()
    {
        cam = Camera.main;
        getTouchWait = new WaitForSeconds(getTouchInterval);
    }

    public void GameStart()
    {
        StartCoroutine(CR_GetTouchPos());
    }

    void Update()
    {
        if (!gravityEffector.OnGround)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }

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
                    if (!GameManager.Instance.gameStarted)
                    {
                        GameManager.Instance.GameStart();
                    }

                    TouchPosToWorldSpace(touch.position);
                    touchHeight = cam.ScreenToWorldPoint(touchPosition).y;
                    break;
                case TouchPhase.Moved:
                    TouchPosToWorldSpace(touch.position);
                    yDistanceFromTouchHeight = cam.ScreenToWorldPoint(touchPosition).y - touchHeight;
                    objectScaler.Scale(yDistanceFromTouchHeight);
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
        touchPosition.z = cam.nearClipPlane;
    }

    public void Reset()
    {
        StopAllCoroutines();
        isActive = true;
    }

    public void SetActive(bool _value) => isActive = _value;
}
