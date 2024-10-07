using System.Collections;
using UnityEngine;
public class CollisionHandler : MonoBehaviour, ITriggerByGoal, ITriggerByObstacle, ITriggerByTurnPath
{
    [Header("Components")]
    [SerializeField] AnimationHandler animationHandler;
    [SerializeField] GravityEffector gravity;
    [SerializeField] InputHandler inputHandler;
    [SerializeField] MovingObject movingObject;
    [SerializeField] TurnHandler turnHandler;
    [Header("Push back settings")]
    [SerializeField] float pushBackSpeedRatio;
    [SerializeField] float restoreSpeedTime;
    [SerializeField] float pushBackCD = 0.1f;
    [SerializeField] bool feverStatus = false;
    bool beingPushback;

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
        movingObject.Stop();
        gravity.enabled = false;
        inputHandler.SetActive(false);
        animationHandler.GoalReach();
    }

    public void TriggerByObstacle()
    {
        if (!feverStatus)
        {
            PushBack();
        }
    }

    public void TriggerByTurnPath(PathDirection _direction, Rotater _rotater, Transform _pivot)
    {
        turnHandler.Turn(_direction, _rotater, _pivot);
    }

    void PushBack()
    {
        if (beingPushback) return;
        StartCoroutine(CR_ResetPushbackStatus());
        StartCoroutine(CR_PushBack());
    }

    IEnumerator CR_ResetPushbackStatus()
    {
        beingPushback = true;
        yield return new WaitForSeconds(pushBackCD);
        beingPushback = false;
    }

    IEnumerator CR_PushBack()
    {
        // store player speed when collide
        float currentSpeed;
        if (!feverStatus) currentSpeed = movingObject.Speed;
        else currentSpeed = movingObject.FeverSpeed;

        // reverse object speed to make it move backward
        movingObject.CurrentSpeed *= pushBackSpeedRatio;

        // slowly restore object speed
        yield return movingObject.ChangeSpeedOvertime(currentSpeed, restoreSpeedTime);
    }
}