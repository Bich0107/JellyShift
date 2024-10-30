using System.IO;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public SaveFile currentSaveFile;
    public GameSettingSO gameSettingFile;
    [SerializeField] string saveFileName = "SaveFile.json";
    [SerializeField] string settingFileName = "GameSetting.json";
    string progressString = "Save";
    string settingString = "Setting";

    protected override void Awake()
    {
        base.Awake();
        LoadSettings();
        LoadSave();
    }

    public void NewGame()
    {
        if (currentSaveFile != null)
        {
            currentSaveFile.Reset();
            PlayerPrefs.DeleteKey(progressString);
        }
        else
        {
            CreateNewSaveFile();
        }

        SaveProgress();
    }

    void LoadSettings()
    {
        string jsonData = PlayerPrefs.GetString(settingString);
        if (!string.IsNullOrEmpty(jsonData))
        {
            if (gameSettingFile != null)
            {
                JsonUtility.FromJsonOverwrite(jsonData, gameSettingFile);
            }
            else
            {
                GameSettingSO scriptableObject = ScriptableObject.CreateInstance<GameSettingSO>();
                JsonUtility.FromJsonOverwrite(jsonData, scriptableObject);
                gameSettingFile = scriptableObject;
            }
        }
    }

    void LoadSave()
    {
        string jsonData = PlayerPrefs.GetString(progressString);

        if (!string.IsNullOrEmpty(jsonData))
        {
            if (currentSaveFile != null)
            {
                JsonUtility.FromJsonOverwrite(jsonData, currentSaveFile);
            }
            else
            {
                SaveFile scriptableObject = ScriptableObject.CreateInstance<SaveFile>();
                JsonUtility.FromJsonOverwrite(jsonData, scriptableObject);
                currentSaveFile = scriptableObject;
            }
        }
    }

    void CreateNewSaveFile()
    {
        // create new save file
        SaveFile newSaveFile = SaveFile.CreateInstance<SaveFile>();
        newSaveFile.Reset();

        // Serialize the ScriptableObject to a JSON string
        string json = JsonUtility.ToJson(newSaveFile);

        // Save the JSON string to a file
        // string path = Application.persistentDataPath + "/" + saveFileName;
        // File.WriteAllText(path, json);

        PlayerPrefs.SetString(progressString, json);

        // set current save file
        currentSaveFile = newSaveFile;
    }

    public void SaveSetting()
    {
        string json = JsonUtility.ToJson(gameSettingFile);
        PlayerPrefs.SetString(settingString, json);
    }

    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(currentSaveFile);
        PlayerPrefs.SetString(progressString, json);
    }

    public void Reset()
    {
        if (currentSaveFile == null) return;

        currentSaveFile.Reset();
    }
}
