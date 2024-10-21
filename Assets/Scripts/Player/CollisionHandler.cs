using System.Collections;
using UnityEngine;
public class CollisionHandler : MonoBehaviour, ITriggerByGoal, ITriggerByObstacle, ITriggerByTurnPath
{
    [Header("Components")]
    [SerializeField] AnimationHandler animationHandler;
    [SerializeField] PredictionBox predictionBox;
    [SerializeField] GravityEffector gravity;
    [SerializeField] InputHandler inputHandler;
    [SerializeField] MovingObject movingObject;
    [SerializeField] TurnHandler turnHandler;
    [SerializeField] FeverSystem fever;
    [Header("Push back settings")]
    [SerializeField] float pushBackSpeedRatio;
    [SerializeField] float restoreSpeedTime;
    [SerializeField] float pushBackCD = 0.1f;
    bool beingPushback;

    void OnTriggerEnter(Collider other)
    {
        ITriggerByPlayer hit = other.GetComponent<ITriggerByPlayer>();
        if (hit == null)
        {
            hit = other.GetComponentInParent<ITriggerByPlayer>();
        }

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
        predictionBox.enabled = false;

        fever.Reset();
        fever.enabled = false;

        animationHandler.GoalReach();

        GameManager.Instance.GameEnd();
    }

    public void TriggerByObstacle()
    {
        if (!fever.IsActive)
        {
            OnObstacleHit();
        }
    }

    public void TriggerByTurnPath(PathDirection _direction, Rotater _rotater, Transform _pivot)
    {
        turnHandler.Turn(_direction, _rotater, _pivot);
    }

    void OnObstacleHit()
    {
        if (beingPushback) return;

        fever.ReduceFever();

        PlayerScoreHandler.Instance.ReduceScore(LevelManager.Instance.CurrentSetting.DamagePerObstacle);
        LifeHandler.Instance.DecreaseLife();
        StartCoroutine(CR_ResetPushbackStatus());
        PushBack();
    }

    IEnumerator CR_ResetPushbackStatus()
    {
        beingPushback = true;
        yield return new WaitForSeconds(pushBackCD);
        beingPushback = false;
    }

    void PushBack()
    {
        // store player speed when collide
        float currentSpeed;
        currentSpeed = movingObject.Speed;

        // reverse object speed to make it move backward, make sure the speed after push back is negative
        movingObject.CurrentSpeed = Mathf.Abs(movingObject.CurrentSpeed) * pushBackSpeedRatio;

        // slowly restore object speed
        movingObject.ChangeSpeedOvertime(currentSpeed, restoreSpeedTime);
    }

    public void Reset()
    {
        StopAllCoroutines();
        predictionBox.enabled = true;
    }
}