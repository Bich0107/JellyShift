using TMPro;
using UnityEngine;

public class PlayerScoreDisplayer : MonoSingleton<PlayerScoreDisplayer>
{
    [SerializeField] TextMeshProUGUI scoreText;
    float score;

    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void SetScore(float _value) => score = _value;

    public void IncreaseScore(float _value)
    {
        score += _value;
        scoreText.text = score.ToString();
    }
}
