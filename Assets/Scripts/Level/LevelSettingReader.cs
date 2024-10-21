using UnityEngine;

public class LevelSettingReader : MonoBehaviour
{
    [SerializeField] MovingObject movingObject;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] PathGenerator pathGenerator;

    void Awake()
    {
        ReadSettings();
    }

    public void ReadSettings()
    {
        LevelSettingSO setting = LevelManager.Instance.CurrentSetting;
        movingObject.Speed = setting.BaseSpeed;
        feverSystem.SetSpeed(setting.FeverSpeed);
        pathGenerator.SetPathAmount(setting.PathAmount);
    }
}
