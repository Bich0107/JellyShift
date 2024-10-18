using UnityEngine;

public class HapticButton : MonoBehaviour
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
            Debug.Log("turn off haptic");
        }
        else
        {
            isOn = true;
            Debug.Log("turn on haptic");
        }

        SaveManager.Instance.currentSaveFile.HapticOn = isOn;
        VibrateManager.Instance.Toggle();
    }
}
