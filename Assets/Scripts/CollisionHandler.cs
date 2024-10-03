using System.Collections;
using UnityEngine;
public class CollisionHandler : MonoBehaviour, ITriggerByGoal, ITriggerByObstacle
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] MovingObject movingObject;
    [Header("Push back settings")]
    [SerializeField] float pushBackSpeedRatio;
    [SerializeField] float restoreSpeedTime;
    [SerializeField] bool feverStatus = false;
    bool isBeingPushback;

    void OnTriggerEnter(Collider other)
    {
        ITriggerByPlayer hit = other.GetComponent<ITriggerByPlayer>();
        if (hit != null)
        {
            hit.TriggerByPlayer();
        }
    }

    public void TriggerByGoal()
    {
        Debug.Log("player reach goal");
        movingObject.Stop();
        inputHandler.SetActive(false);
    }

    public void TriggerByObstacle()
    {
        if (!feverStatus)
        {
            PushBack();
        }
    }

    void PushBack()
    {
        if (isBeingPushback) return;

        isBeingPushback = true;
        StartCoroutine(CR_PushBack());
    }

    IEnumerator CR_PushBack()
    {
        float currentSpeed = movingObject.CurrentSpeed;
        movingObject.CurrentSpeed *= pushBackSpeedRatio; // reverse object speed to make it move backward

        // slowly restore object speed
        yield return StartCoroutine(movingObject.CR_ChangeSpeed(currentSpeed, restoreSpeedTime));
        isBeingPushback = false;
    }
}