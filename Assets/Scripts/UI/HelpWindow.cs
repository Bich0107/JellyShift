using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] TransformAnimation helpWindowAnimation;

    public void OnClick()
    {
        helpWindowAnimation.Play();
    }

    public void Return()
    {
        helpWindowAnimation.Rewind();
    }
}
