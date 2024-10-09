using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject playCanvas;
    [SerializeField] GameObject endGameCanvas;

    public void GameStart()
    {
        playCanvas.SetActive(true);
        endGameCanvas.SetActive(false);
    }

    public void GameEnd()
    {
        playCanvas.SetActive(false);
        endGameCanvas.SetActive(true);
    }

    public void GameWait()
    {
        playCanvas.SetActive(false);
        endGameCanvas.SetActive(false);
    }
}
