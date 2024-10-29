using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayer : MonoBehaviour
{
    [SerializeField] Image lifeBar;

    public void Display(int _value)
    {
        lifeBar.fillAmount = (float)_value / SaveFile.s_MaxLife;
    }
}
