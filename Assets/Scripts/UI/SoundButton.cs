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
        if (isOn)
        {
            isOn = false;
            Debug.Log("turn off sound");
        }
        else
        {
            isOn = true;
            Debug.Log("turn on sound");
        }

        SaveManager.Instance.gameSettingFile.SoundOn = isOn;
        SoundManager.Instance.Toggle();
    }
}
