using UnityEngine;
using UnityEngine.UI;

public class ButtonSfxSetter : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Button[] buttons;
    [SerializeField] AudioClip touchSfx;

    void Awake()
    {
        SetSound();
    }

    void SetSound()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() =>
            {
                audioSource.PlayOneShot(touchSfx);
            });
        }
    }
}
