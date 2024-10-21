using TMPro;
using UnityEngine;

public class PlayerScoreHandler : MonoSingleton<PlayerScoreHandler>
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score;

    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void SetScore(int _value) => score = _value;

    public void IncreaseScore(int _value)
    {
        score += _value;
        scoreText.text = score.ToString();
        SaveManager.Instance.currentSaveFile.Score = score;
    }

    public void ReduceScore(int _value)
    {
        score -= _value;
        if (score < 0) score = 0;
        scoreText.text = score.ToString();
        SaveManager.Instance.currentSaveFile.Score = score;
    }
}
