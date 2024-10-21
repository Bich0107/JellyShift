using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoSingleton<ScoreKeeper>
{
    [SerializeField] int[] scores;

    public void SetScores(int[] _values)
    {
        scores = new int[_values.Length];
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = _values[i];
        }
    }

    public void AddScore(int _value)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < _value)
            {
                for (int j = scores.Length - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }

                scores[i] = _value;
                break;
            }
        }

        UpdateSaveFile();
    }

    void UpdateSaveFile()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            SaveManager.Instance.currentSaveFile.HighScores[i] = scores[i];
        }

    }

    public bool IsHighScore(int _value)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < _value)
            {
                return true;
            }
        }

        return false;
    }
}
