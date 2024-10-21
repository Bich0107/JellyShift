using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] Button button;
    SaveFile currentSaveFile => SaveManager.Instance.currentSaveFile;

    void Start()
    {
        if (currentSaveFile == null) button.interactable = false;
        else button.interactable = true;
    }

    public void OnClick()
    {
        if (currentSaveFile != null)
        {
            SceneManager.LoadScene(1);
        }
    }
}
