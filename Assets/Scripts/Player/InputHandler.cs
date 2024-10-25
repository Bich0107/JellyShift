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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveManager.Instance.currentSaveFile.Life = 3;
        }
#endif

        //  HandleTouch();
    }
}
