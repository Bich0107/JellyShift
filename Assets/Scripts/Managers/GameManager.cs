using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public bool gameStarted = false;
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] PathGenerator pathGenerator;
    [SerializeField] CameraStateManager camStateManager;
    [SerializeField] DistanceBar distanceBar;
    [SerializeField] Player player;
    [SerializeField] InterstitialAdDisplayer adDisplayer;
    [SerializeField] LevelSettingReader settingReader;
    [SerializeField] ButtonGroup buttonGroup;
    bool gameOver = false;
    bool levelFinished = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void GameStart()
    {
        if (gameStarted) return;

        levelFinished = false;
        gameStarted = true;

        // restore player to full health at the start of each level
        LifeHandler.Instance.IncreaseLife(999);

        canvasManager.GameStart();

        // turn on button
        buttonGroup.SetStatus(true);

        player.GameStart();

        camStateManager.ChangeState(CameraState.Follow);
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // when player finished a level
    public void GameEnd()
    {
        if (gameOver) return;

        gameOver = true;
        Crystal.ResetCounter();
        adDisplayer.UpdateCounter();
        levelFinished = true;
        canvasManager.GameEnd();
        camStateManager.ChangeState(CameraState.Rotate);
    }

    // when player lost
    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;

        Crystal.ResetCounter();
        Time.timeScale = 0f;

        SaveManager.Instance.currentSaveFile.GameOver = true;
        ScoreKeeper.Instance.AddScore(SaveManager.Instance.currentSaveFile.Score);
        adDisplayer.UpdateCounter();
        canvasManager.GameOver();
        camStateManager.ChangeState(CameraState.Idle);
    }

    public void Replay()
    {
        if (!gameOver) return;

        gameStarted = false;
        gameOver = false;

        canvasManager.GameWait();

        if (levelFinished)
        {
            LevelManager.Instance.IncreaseLevel();
            settingReader.ReadSettings();
        }

        distanceBar.Reset();
        ObjectPool.Instance.Reset();
        pathGenerator.Reset();
        pathGenerator.GeneratePaths();

        camStateManager.Reset();

        player.Reset();

        SaveManager.Instance.SaveProgress();

        adDisplayer.ShowInterstitialAd();
    }
}
