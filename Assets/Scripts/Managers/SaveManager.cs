using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public SaveFile currentSaveFile;

    protected override void Awake()
    {
        base.Awake();
        CreateNewSaveFile();
        currentSaveFile = LoadSave();
    }

    SaveFile LoadSave()
    {
        string jsonData = PlayerPrefs.GetString("Save");

        if (!string.IsNullOrEmpty(jsonData))
        {
            SaveFile scriptableObject = ScriptableObject.CreateInstance<SaveFile>();
            JsonUtility.FromJsonOverwrite(jsonData, scriptableObject);
            return scriptableObject;
        }
        else
        {
            return null;
        }
    }

    void CreateNewSaveFile()
    {
        SaveFile newSaveFile = SaveFile.CreateInstance<SaveFile>();
        newSaveFile.Reset();

        // Serialize the ScriptableObject to a JSON string
        string json = JsonUtility.ToJson(newSaveFile);

        // set current save file
        currentSaveFile = newSaveFile;

        PlayerPrefs.SetString("Save", json);
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        if (currentSaveFile == null) return;

        currentSaveFile.Reset();
    }
}
