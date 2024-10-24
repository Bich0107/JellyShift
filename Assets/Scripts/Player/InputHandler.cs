using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveManager.Instance.Reset();
        }
#endif

        HandleTouch();
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (IsTouchOnUI(touch)) return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (!GameManager.Instance.gameStarted)
                    {
                        GameManager.Instance.GameStart();
                    }

                    break;
            }
        }
    }

    bool IsTouchOnUI(Touch touch)
    {
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            // The touch is on a UI element
            return true;
        }

        return false;
    }

    public void Reset()
    {
        StopAllCoroutines();
    }
}
