using DG.Tweening;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool gameStarted = false;
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] PathGenerator pathGenerator;
    [SerializeField] CameraStateManager camStateManager;
    [SerializeField] DistanceBar distanceBar;
    [SerializeField] Player player;
    [SerializeField] InterstitialAdDisplayer adDisplayer;
    bool gameOver = false;
    bool levelFinished = false;

    protected override void Awake()
    {
        base.Awake();
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;
    }

    public void GameStart()
    {
        levelFinished = false;
        gameStarted = true;

        canvasManager.GameStart();
        player.GameStart();

        camStateManager.ChangeState(CameraState.Follow);
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
        canvasManager.GamePause();
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        canvasManager.GameResume();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        Replay();
    }

    // when player finished a level
    public void GameEnd()
    {
        if (gameOver) return;

        adDisplayer.UpdateCounter();
        levelFinished = true;
        gameOver = true;
        canvasManager.GameEnd();
        camStateManager.ChangeState(CameraState.Rotate);
    }

    // when player fail to finished a level
    public void GameOver()
    {
        if (gameOver) return;

        adDisplayer.UpdateCounter();
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

        adDisplayer.ShowInterstitialAd();
    }
}
