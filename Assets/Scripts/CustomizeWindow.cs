using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeWindow : MonoBehaviour
{
    [SerializeField] GameObject customizeWindow;

    public void Open()
    {
        customizeWindow.SetActive(true);
    }

    public void Close()
    {
        customizeWindow.SetActive(false);
    }
}
