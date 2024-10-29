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
        endGamePanelAnimation.Rewind();
        gameOverPanelAnimation.Rewind();
        pauseCanvasAnimation.Rewind();
    }

    public void GameEnd()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        pauseCanvasAnimation.Rewind();
        gameOverPanelAnimation.Rewind();
    }

    public void GameOver()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        endGamePanelAnimation.Rewind();
        pauseCanvasAnimation.Rewind();
    }

    public void GameWait()
    {
        startCanvas.SetActive(true);
        playCanvas.SetActive(false);
        endGamePanelAnimation.Rewind();
        gameOverPanelAnimation.Rewind();
        pauseCanvasAnimation.Rewind();
    }
}
