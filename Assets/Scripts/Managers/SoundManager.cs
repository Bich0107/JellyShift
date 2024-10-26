using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] SoundButton soundButton;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float normalVolume;
    [SerializeField] float muteVolume;
    bool isOn;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetStatus(bool _status)
    {
        isOn = _status;
        if (_status)
        {
            audioMixer.SetFloat("Volume", normalVolume);
        }
        else
        {
            audioMixer.SetFloat("Volume", muteVolume);
        }

        float temp;
        audioMixer.GetFloat("Volume", out temp);
        Debug.Log("Turn " + (isOn ? "on" : "off") + "-Volume: " + temp);

        soundButton.SetStatus(isOn);
    }

    public void Toggle()
    {
        isOn = !isOn;

        if (isOn)
        {
            audioMixer.SetFloat("Volume", normalVolume);
        }
        else
        {
            audioMixer.SetFloat("Volume", muteVolume);
        }

        float temp;
        audioMixer.GetFloat("Volume", out temp);
        Debug.Log("Turn " + (isOn ? "on" : "off") + "-Volume: " + temp);
    }
}
