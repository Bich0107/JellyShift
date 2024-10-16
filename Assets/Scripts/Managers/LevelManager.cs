using TMPro;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] PathGenerator pathGenerator;
    [SerializeField] int currentLevel;
    [SerializeField] int pathAddPerLevel;
    [SerializeField] int minPathAmount;
    [SerializeField] int maxPathAmount;
    [SerializeField] TextMeshProUGUI levelText;

    protected override void Awake()
    {
        base.Awake();

        pathGenerator.SetPathAmount(GetPathAmount());
    }

    public void SetLevel(int _value) => currentLevel = _value;

    public void IncreaseLevel()
    {
        currentLevel++;
        pathGenerator.SetPathAmount(GetPathAmount());
        levelText.text = "LEVEL " + currentLevel;

        SaveManager.Instance.currentSaveFile.Level = currentLevel;
    }

    int GetPathAmount()
    {
        return Mathf.Min(minPathAmount + pathAddPerLevel * (currentLevel - 1), maxPathAmount);
    }

    public void Reset()
    {
        currentLevel = 1;
    }
}
