using UnityEngine;

public class Goal : MonoBehaviour
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
        ITriggerByGoal hit = other.GetComponentInParent<ITriggerByGoal>();
        if (hit != null)
        {
            hit.TriggerByGoal();
        }
    }
}
