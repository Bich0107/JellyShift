using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject playCanvas;
    [SerializeField] AnimationSequence pauseCanvasAnimation;
    [SerializeField] AnimationSequence endGamePanelAnimation;
    [SerializeField] AnimationSequence gameOverPanelAnimation;

    public void Start()
    {
        GameWait();
    }

    public void GameStart()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(true);
        endGamePanelAnimation.Reset();
        gameOverPanelAnimation.Reset();
        pauseCanvasAnimation.Reset();
    }

    public void GameEnd()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        pauseCanvasAnimation.Reset();
        gameOverPanelAnimation.Reset();
        endGamePanelAnimation.Play();
    }

    public void GameOver()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        endGamePanelAnimation.Reset();
        pauseCanvasAnimation.Reset();
        gameOverPanelAnimation.Play();
    }

    public void GameWait()
    {
        startCanvas.SetActive(true);
        playCanvas.SetActive(false);
        endGamePanelAnimation.Reset();
        gameOverPanelAnimation.Reset();
        pauseCanvasAnimation.Reset();
    }
}
