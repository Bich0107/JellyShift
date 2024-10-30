using System.Collections;
using UnityEngine;
public class CollisionHandler : MonoBehaviour, ITriggerByGoal, ITriggerByObstacle, ITriggerByTurnPath
{
    [Header("Components")]
    [SerializeField] AnimationHandler animationHandler;
    [SerializeField] ShapeShifter shapeShifter;
    [SerializeField] GravityEffector gravity;
    [SerializeField] MovingObject movingObject;
    [SerializeField] TurnHandler turnHandler;
    [SerializeField] FeverSystem fever;
    [SerializeField] FloatButton floatButton;
    [SerializeField] SoundHandler soundHandler;
    [SerializeField] ButtonGroup buttonGroup;
    [Header("Push back settings")]
    [SerializeField] float pushBackSpeedRatio;
    [SerializeField] float restoreSpeedTime;
    [SerializeField] float pushBackCD = 0.1f;
    bool beingPushback;
    bool triggeredByGoal = false;

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
        if (triggeredByGoal) return;

        triggeredByGoal = true;
        soundHandler.ReachGoal();
        floatButton.Reset();
        movingObject.Stop();
        shapeShifter.ShapeShift(ShapeType.Cube);
        gravity.enabled = false;

        // turn off buttons
        buttonGroup.SetStatus(false);

        fever.Reset();
        fever.enabled = false;

        animationHandler.GoalReach();
        GameManager.Instance.GameEnd();
    }

    public void TriggerByObstacle()
    {
        if (fever.IsActive) return;

        if (beingPushback) return;

        soundHandler.CollideWIthObstacle();
        fever.ReduceFever();

        PlayerScoreHandler.Instance.ReduceScore(LevelManager.Instance.CurrentSetting.ScoreDecreasePerObstacle);
        LifeHandler.Instance.DecreaseLife(LevelManager.Instance.CurrentSetting.DamagePerObstacle);
        StartCoroutine(CR_ResetPushbackStatus());
        PushBack();
    }

    public void TriggerByTurnPath(PathDirection _direction, Rotater _rotater, Transform _pivot)
    {
        turnHandler.Turn(_direction, _rotater, _pivot);
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
        triggeredByGoal = false;
    }
}