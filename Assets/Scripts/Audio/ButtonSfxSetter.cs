using UnityEngine;
using UnityEngine.UI;

public class ButtonSfxSetter : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Button[] buttons;
    [SerializeField] AudioClip touchSfx;
    [SerializeField] GameSettingSO gameSetting;

    void Awake()
    {
        gameSetting = SaveManager.Instance.gameSettingFile;
        SetSound();
    }

    void SetSound()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() =>
            {
                if (!gameSetting.SoundOn) return;

                audioSource.PlayOneShot(touchSfx);
            });
        }
    }
}
