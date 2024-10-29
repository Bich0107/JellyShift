using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDisplayer : MonoBehaviour
{
    [SerializeField] GameObject[] lifes;

    public void Display(int _value)
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(i < _value);
        }
    }
}
