using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyButton : MonoBehaviour
{
    [SerializeField] string content;

    public void OnClick()
    {
        GUIUtility.systemCopyBuffer = content;
    }
}
