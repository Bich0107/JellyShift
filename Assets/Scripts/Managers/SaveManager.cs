using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public SaveFile currentSaveFile;
    [SerializeField] string saveFileName = "SaveFile.json";

    protected override void Awake()
    {
        base.Awake();
    }

    public void NewGame()
    {
        CreateNewSaveFile();
    }

    public void Continue()
    {
        if (currentSaveFile == null) currentSaveFile = LoadSave();
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

    SaveFile LoadSaveFile()
    {
        string filePath = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveFile scriptableObject = ScriptableObject.CreateInstance<SaveFile>();
            JsonUtility.FromJsonOverwrite(json, scriptableObject);
            Debug.Log("ScriptableObject loaded from " + filePath);
            return scriptableObject;
        }
        else
        {
            Debug.LogError("File not found at " + filePath);
            return null;
        }
    }

    public void CreateNewSaveFile()
    {
        // create new save file
        SaveFile newSaveFile = SaveFile.CreateInstance<SaveFile>();
        newSaveFile.Reset();

        // Serialize the ScriptableObject to a JSON string
        string json = JsonUtility.ToJson(newSaveFile);

        // Save the JSON string to a file
        string path = Application.persistentDataPath + "/" + saveFileName;
        File.WriteAllText(path, json);

        // set current save file
        currentSaveFile = newSaveFile;
    }

    public void Reset()
    {
        if (currentSaveFile == null) return;

        currentSaveFile.Reset();
    }
}
