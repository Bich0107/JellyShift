using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GravityEffector gravityEffector;
    [SerializeField] MovingObject movingObject;
    [SerializeField] AnimationHandler animationHandler;
    [SerializeField] TurnHandler turnHandler;
    [SerializeField] CollisionHandler collisionHandler;
    [SerializeField] HeightChecker heightChecker;
    Vector3 basePosition;
    FeverSystem fever;

    [SerializeField] PlayerSkinSO skin;
    public PlayerSkinSO Skin
    {
        get { return skin; }
        set { skin = value; }
    }

    void Awake()
    {
        basePosition = transform.position;
        fever = FindObjectOfType<FeverSystem>();
    }

    public void GameStart()
    {
        animationHandler.Reset();
        movingObject.Move();
        turnHandler.Turn(new Vector3(0f, 180f, 0f));
    }

    public void Reset()
    {
        transform.parent = null;

        gravityEffector.enabled = true;
        gravityEffector.Reset();

        movingObject.Reset();
        animationHandler.Reset();
        turnHandler.Reset();
        collisionHandler.Reset();
        heightChecker.Reset();

        fever.enabled = true;
        fever.Reset();

        transform.position = basePosition;
        animationHandler.PlayIdleAnimation();
    }
}
