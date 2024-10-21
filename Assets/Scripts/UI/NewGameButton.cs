using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] WindowAnimation confirmWindow;
    SaveFile currentSaveFile => SaveManager.Instance.currentSaveFile;


    public void OnClick()
    {
        if (currentSaveFile == null)
        {
            SaveManager.Instance.NewGame();
            SceneManager.LoadScene(1);
        }
        else
        {
            confirmWindow.Play();
        }
    }

    public void NewGame()
    {
        SaveManager.Instance.NewGame();
        SceneManager.LoadScene(1);
    }

    public void Return()
    {
        confirmWindow.Rewind();
    }
}
