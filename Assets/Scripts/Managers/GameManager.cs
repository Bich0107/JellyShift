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

    protected override void Awake()
    {
        base.Awake();
    }

    public void GameStart()
    {
        gameStarted = true;

        canvasManager.GameStart();
        player.GameStart();

        camStateManager.ChangeState(CameraState.Follow);
    }

    public void GameEnd()
    {
        canvasManager.GameEnd();
        camStateManager.ChangeState(CameraState.Rotate);
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void Replay()
    {
        gameStarted = false;

        canvasManager.GameWait();

        distanceBar.Reset();
        ObjectPool.Instance.Reset();
        pathGenerator.Reset();
        pathGenerator.GeneratePaths();

        camStateManager.Reset();

        player.Reset();
    }
}
