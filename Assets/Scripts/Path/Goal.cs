using UnityEngine;

public class Goal : MonoBehaviour, ITriggerByPlayer
{
    bool isTriggered;

    public void TriggerByPlayer()
    {
        if (isTriggered) return;
        isTriggered = true;

        // add some vfx here
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        ITriggerByGoal hit = other.GetComponentInParent<ITriggerByGoal>();
        if (hit != null)
        {
            hit.TriggerByGoal();
        }
    }
}
