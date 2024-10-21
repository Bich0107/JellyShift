using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game settings", menuName = "Game setting file")]
public class GameSettingSO : ScriptableObject
{
    public int[] HighScores;
    public bool HapticOn;
    public bool SoundOn;

    public void Reset()
    {
        HapticOn = true;
        SoundOn = true;
    }
}
