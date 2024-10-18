using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class VibrateManager : MonoSingleton<VibrateManager>
{
    [SerializeField] HapticButton hapticButton;
    bool isOn;
    public bool IsOn => isOn;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetStatus(bool _status)
    {
        isOn = _status;
        hapticButton.SetStatus(isOn);
    }

    public void Toggle() => isOn = !isOn;

    public void Vibrate()
    {
        if (!isOn) return;
        Handheld.Vibrate();
    }
}
