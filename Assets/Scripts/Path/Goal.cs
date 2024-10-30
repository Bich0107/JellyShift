using UnityEngine;

public class Goal : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] GameObject goalReachVFX;
    bool isTriggered;

    public void TriggerByPlayer()
    {
        if (isTriggered) return;
        isTriggered = true;

        goalReachVFX.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        ITriggerByGoal hit = other.GetComponentInParent<ITriggerByGoal>();
        if (hit != null)
        {
            hit.TriggerByGoal();
        }
    }

    void OnDisable()
    {
        goalReachVFX.SetActive(false);
        isTriggered = false;
    }
}
