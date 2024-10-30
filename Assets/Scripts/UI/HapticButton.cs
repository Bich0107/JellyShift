using UnityEngine;

public class HapticButton : MonoBehaviour
{
    bool isOn = true;
    [SerializeField] SpriteToggleScript toggleScript;

    void Start()
    {
        GameSettingSO gameSettingSO = SaveManager.Instance.gameSettingFile;
        SetStatus(gameSettingSO.HapticOn);
    }

    public void SetStatus(bool _status)
    {
        isOn = _status;
        toggleScript.SetStatus(isOn);
    }

    public void OnClick()
    {
        isOn = !isOn;

        SaveManager.Instance.gameSettingFile.HapticOn = isOn;
        VibrateManager.Instance.Toggle();
        SaveManager.Instance.SaveSetting();
    }
}
