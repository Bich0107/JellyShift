using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanelButton : MonoBehaviour
{
    public GameObject target;

    public void OnClick()
    {
        target.SetActive(false);
    }
}
