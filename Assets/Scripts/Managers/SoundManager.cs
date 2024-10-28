using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float normalVolume;
    [SerializeField] float muteVolume;
    string volumeString = "Volume";
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
            audioMixer.SetFloat(volumeString, normalVolume);
        }
        else
        {
            audioMixer.SetFloat(volumeString, muteVolume);
        }
    }

    public void Toggle()
    {
        isOn = !isOn;

        if (isOn)
        {
            audioMixer.SetFloat(volumeString, normalVolume);
        }
        else
        {
            audioMixer.SetFloat(volumeString, muteVolume);
        }
    }
}
