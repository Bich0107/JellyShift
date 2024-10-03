using UnityEngine;

public class Goal : MonoBehaviour, ITriggerByPlayer
{
    bool isTriggered;

    public void TriggerByPlayer()
    {
        if (isTriggered) return;

        isTriggered = true;
    }

    void OnTriggerEnter(Collider other)
    {
        ITriggerByGoal hit = other.GetComponent<ITriggerByGoal>();
        if (hit != null)
        {
            hit.TriggerByGoal();
        }
    }
}
