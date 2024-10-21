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
        Bank.Instance.SetCrystalAmount(saveFile.Crystal);
        LevelManager.Instance.SetLevel(saveFile.Level);
        SoundManager.Instance.SetStatus(saveFile.SoundOn);
        VibrateManager.Instance.SetStatus(saveFile.HapticOn);
        PlayerScoreHandler.Instance.SetScore(saveFile.Score);
        ScoreKeeper.Instance.SetScores(saveFile.HighScores);
    }
}
