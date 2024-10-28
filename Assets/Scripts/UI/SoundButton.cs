using UnityEngine;

public class SoundButton : MonoBehaviour
{
    bool isOn = true;
    [SerializeField] SpriteToggleScript toggleScript;

    void Start()
    {
        GameSettingSO gameSettingSO = SaveManager.Instance.gameSettingFile;
        SetStatus(gameSettingSO.SoundOn);
    }

    public void SetStatus(bool _status)
    {
        isOn = _status;
        toggleScript.SetStatus(isOn);
    }

    public void OnClick()
    {
        isOn = !isOn;

        SaveManager.Instance.gameSettingFile.SoundOn = isOn;
        SoundManager.Instance.Toggle();
    }
}
