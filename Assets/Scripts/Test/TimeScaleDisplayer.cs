using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScaleDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    string temp = "current time scale: ";

    void Update()
    {
        text.text = temp + Time.timeScale;
    }
}
