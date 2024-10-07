using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    [SerializeField] MovingObject movingObject;
    [SerializeField] Rotater camRotater;
    [SerializeField] Rotater playerRotater;
    [SerializeField] float duration;
    WaitForSeconds turnWait;

    void Awake()
    {
        playerTrans = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        turnWait = new WaitForSeconds(duration);
    }

    public void Turn(Vector3 _angle, bool _moveAfterTurn = true)
    {
        StartCoroutine(CR_PlayerMovemenControlSequence(_moveAfterTurn));
        Quaternion rotation = Quaternion.Euler(_angle);
        playerRotater.Rotate(rotation, duration);
    }

    public void Turn(PathDirection _direction, Rotater _rotater, Transform _pivot)
    {
        Quaternion rotation;
        switch (_direction)
        {
            case PathDirection.Left:
                rotation = Quaternion.AngleAxis(-90f, Vector3.up);
                break;
            case PathDirection.Right:
                rotation = Quaternion.AngleAxis(90f, Vector3.up);
                break;
            default:
                rotation = Quaternion.identity;
                break;
        }

        // stop player and wait until the cam and turn path corner finish rotating
        // then remove player parent and start moving again
        StartCoroutine(CR_PlayerMovemenControlSequence());

        // rotate camera
        camRotater.Rotate(rotation, duration);

        // set player to be child of the pivot (turn path's corner) to rotate it along the path
        playerTrans.parent = _pivot;
        _rotater.Rotate(rotation, duration);
    }

    IEnumerator CR_PlayerMovemenControlSequence(bool _moveAgain = true)
    {
        movingObject.Stop();
        yield return turnWait;
        playerTrans.parent = null;

        if (_moveAgain) movingObject.Move();
    }
}
