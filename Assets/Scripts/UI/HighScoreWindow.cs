using TMPro;
using UnityEngine;

public class HighScoreWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] indexTexts;
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] WindowAnimation highscoreWindowAnimation;

    public void Display()
    {
        SetScores();
        highscoreWindowAnimation.Play();
    }

    void SetScores()
    {
        int[] scores = SaveManager.Instance.currentSaveFile.HighScores;
        for (int i = 0; i < scores.Length; i++)
        {
            indexTexts[i].text = (i + 1).ToString();
            scoreTexts[i].text = scores[i].ToString();
        }
    }

    public void Return()
    {
        highscoreWindowAnimation.Rewind();
    }
}
