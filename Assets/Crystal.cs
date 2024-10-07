using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerByPlayer
{
    public void TriggerByPlayer()
    {
        Debug.Log("Trigger crystal");
        gameObject.SetActive(false);
    }
}
