using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoSingleton<AdManager>
{
    public void ShowAd()
    {
        Debug.Log("ad showed");
    }
}
