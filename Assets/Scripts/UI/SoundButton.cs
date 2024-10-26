using UnityEngine;

public class SoundButton : MonoBehaviour
{
    bool isOn = true;
    [SerializeField] SpriteToggleScript toggleScript;

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
