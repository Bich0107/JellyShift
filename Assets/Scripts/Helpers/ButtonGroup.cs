using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    public void SetStatus(bool _status)
    {
        foreach (Button button in buttons)
        {
            button.interactable = _status;
        }
    }
}
