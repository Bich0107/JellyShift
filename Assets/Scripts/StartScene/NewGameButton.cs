using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    SaveFile currentSaveFile => SaveManager.Instance.currentSaveFile;

    public void OnClick()
    {
        if (currentSaveFile == null)
        {
            SaveManager.Instance.CreateNewSaveFile();
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("A save file is already exist");
        }
    }
}
