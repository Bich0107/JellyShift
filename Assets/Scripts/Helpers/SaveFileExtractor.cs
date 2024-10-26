using UnityEngine;

public class SaveFileExtractor : MonoBehaviour
{
    void Awake()
    {
        ExtractSaveFile();
    }

    void Start()
    {
        // for unknown reason, setting volume in awake is too early for audio mixer.
        GameSettingSO gameSettingSO = SaveManager.Instance.gameSettingFile;
        SoundManager.Instance.SetStatus(gameSettingSO.SoundOn);
    }

    void ExtractSaveFile()
    {
        SaveFile saveFile = SaveManager.Instance.currentSaveFile;

        Bank.Instance.SetCrystalAmount(saveFile.Crystal);
        LevelManager.Instance.SetLevel(saveFile.Level);
        PlayerScoreHandler.Instance.SetScore(saveFile.Score);

        GameSettingSO gameSettingSO = SaveManager.Instance.gameSettingFile;

        ScoreKeeper.Instance.SetScores(gameSettingSO.HighScores);
        VibrateManager.Instance.SetStatus(gameSettingSO.HapticOn);
    }
}
