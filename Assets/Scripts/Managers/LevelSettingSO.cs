using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level settings", menuName = "Level setting file")]
public class LevelSettingSO : ScriptableObject
{
    [Header("Level range")]
    [SerializeField] int startLevel;
    [SerializeField] int endLevel;
    [Space]
    [SerializeField] float baseSpeed;
    [SerializeField] float feverSpeed;
    [SerializeField] float scorePerObstacle;
    [SerializeField] float damagePerObstacle;
    [SerializeField] int pathAmount;

    public float BaseSpeed => baseSpeed;
    public float FeverSpeed => baseSpeed;
    public float ScorePerObstacle => scorePerObstacle;
    public float DamagePerObstacle => damagePerObstacle;
    public int PathAmount => pathAmount;

    public bool Contains(int _value)
    {
        return _value <= endLevel && _value >= startLevel;
    }
}
