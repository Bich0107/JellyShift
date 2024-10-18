using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject playCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] WindowAnimation pauseCanvasAnimation;
    [SerializeField] GameObject endGameCanvas;
    [SerializeField] GameObject gameOverCanvas;

    public void Start()
    {
        GameWait();
    }

    public void GameStart()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(true);
        endGameCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        pauseCanvasAnimation.Reset();
        gameOverCanvas.SetActive(false);
    }

    public void GameEnd()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        pauseCanvasAnimation.Reset();
        endGameCanvas.SetActive(true);
    }

    public void GameOver()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        endGameCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        pauseCanvasAnimation.Reset();
        gameOverCanvas.SetActive(true);
    }

    public void GameWait()
    {
        startCanvas.SetActive(true);
        playCanvas.SetActive(false);
        endGameCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        pauseCanvasAnimation.Reset();
        gameOverCanvas.SetActive(false);
    }

    public void GamePause()
    {
        pauseCanvas.SetActive(true);
    }

    public void GameResume()
    {
        pauseCanvas.SetActive(false);
    }
}
