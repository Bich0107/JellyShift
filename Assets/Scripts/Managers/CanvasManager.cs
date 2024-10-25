using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject playCanvas;
    [SerializeField] AnimationSequence pauseCanvasAnimation;
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
        pauseCanvasAnimation.Rewind();
        gameOverCanvas.SetActive(false);
    }

    public void GameEnd()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        pauseCanvasAnimation.Rewind();
        endGameCanvas.SetActive(true);
    }

    public void GameOver()
    {
        startCanvas.SetActive(false);
        playCanvas.SetActive(false);
        endGameCanvas.SetActive(false);
        pauseCanvasAnimation.Rewind();
        gameOverCanvas.SetActive(true);
    }

    public void GameWait()
    {
        startCanvas.SetActive(true);
        playCanvas.SetActive(false);
        endGameCanvas.SetActive(false);
        pauseCanvasAnimation.Rewind();
        gameOverCanvas.SetActive(false);
    }
}
