using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] SoundButton soundButton;
    bool isOn;
    public bool IsOn => isOn;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetStatus(bool _status)
    {
        isOn = _status;
        soundButton.SetStatus(isOn);
    }

    public void Toggle() => isOn = !isOn;

    public void PlaySound(AudioClip _clip)
    {
        if (!isOn) return;
        sfxSource.PlayOneShot(_clip);
    }

}
