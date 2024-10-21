using UnityEngine;

public class SaveFileExtractor : MonoBehaviour
{
    void Awake()
    {
        ExtractSaveFile();
    }

    void ExtractSaveFile()
    {
        SaveFile saveFile = SaveManager.Instance.currentSaveFile;
        GameSettingSO gameSettingSO = SaveManager.Instance.gameSettingFile;
        Bank.Instance.SetCrystalAmount(saveFile.Crystal);
        LevelManager.Instance.SetLevel(saveFile.Level);
        PlayerScoreHandler.Instance.SetScore(saveFile.Score);

        ScoreKeeper.Instance.SetScores(gameSettingSO.HighScores);
        SoundManager.Instance.SetStatus(gameSettingSO.SoundOn);
        VibrateManager.Instance.SetStatus(gameSettingSO.HapticOn);
    }
}
