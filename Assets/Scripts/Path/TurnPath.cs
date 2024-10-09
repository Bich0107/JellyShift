using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPath : BasePath
{
    [SerializeField] Transform turnCorner;
    [SerializeField] Rotater cornerRotater;
    bool isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        ITriggerByTurnPath hit = other.GetComponentInParent<ITriggerByTurnPath>();
        if (hit != null)
        {
            isTriggered = true;
            hit.TriggerByTurnPath(PathDirection, cornerRotater, turnCorner);
        }
    }

    void OnDisable()
    {
        isTriggered = false;
    }
}
