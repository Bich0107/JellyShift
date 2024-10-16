using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool gameStarted = false;
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] PathGenerator pathGenerator;
    [SerializeField] CameraStateManager camStateManager;
    [SerializeField] DistanceBar distanceBar;
    [SerializeField] Player player;
    bool gameOver = false;
    bool levelFinished = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void GameStart()
    {
        levelFinished = false;
        gameStarted = true;

        canvasManager.GameStart();
        player.GameStart();

        camStateManager.ChangeState(CameraState.Follow);
    }

    public void GameEnd()
    {
        if (gameOver) return;

        levelFinished = true;
        gameOver = true;
        canvasManager.GameEnd();
        camStateManager.ChangeState(CameraState.Rotate);
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        canvasManager.GameOver();
        camStateManager.ChangeState(CameraState.Idle);
    }

    public void Replay()
    {
        gameStarted = false;
        gameOver = false;

        canvasManager.GameWait();

        if (levelFinished) LevelManager.Instance.IncreaseLevel();

        distanceBar.Reset();
        ObjectPool.Instance.Reset();
        pathGenerator.Reset();
        pathGenerator.GeneratePaths();

        camStateManager.Reset();

        player.Reset();
    }
}
