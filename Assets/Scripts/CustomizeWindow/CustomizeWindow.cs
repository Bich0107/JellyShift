using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeWindow : MonoBehaviour
{
    [SerializeField] GameObject customizeWindow;

    void Start()
    {
        Close();
    }

    public void Open()
    {
        customizeWindow.SetActive(true);
    }

    public void Close()
    {
        customizeWindow.SetActive(false);
    }
}
