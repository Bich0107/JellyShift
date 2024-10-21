using TMPro;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] PathGenerator pathGenerator;
    [SerializeField] LevelSettingSO[] levelSettings;
    [SerializeField] int currentLevel;
    [SerializeField] TextMeshProUGUI levelText;
    LevelSettingSO currentSetting;
    public LevelSettingSO CurrentSetting => currentSetting;

    protected override void Awake()
    {
        base.Awake();

        GetSettingFile();
        pathGenerator.SetPathAmount(currentSetting.PathAmount);
    }

    void GetSettingFile()
    {
        for (int i = 0; i < levelSettings.Length; i++)
        {
            if (levelSettings[i].Contains(currentLevel))
            {
                currentSetting = levelSettings[i];
                return;
            }
        }
    }

    public void SetLevel(int _value) => currentLevel = _value;

    public void IncreaseLevel()
    {
        currentLevel++;
        pathGenerator.SetPathAmount(currentSetting.PathAmount);
        levelText.text = "LEVEL " + currentLevel;

        SaveManager.Instance.currentSaveFile.Level = currentLevel;
        GetSettingFile();
    }

    public void Reset()
    {
        currentLevel = 1;
    }
}
