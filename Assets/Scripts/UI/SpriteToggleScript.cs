using UnityEngine;
using UnityEngine.UI;

public class SpriteToggleScript : MonoBehaviour
{
    [SerializeField] Image targetImage;
    [SerializeField] Sprite buttonOffSprite;
    [SerializeField] Sprite buttonOnSprite;
    bool isOn = true;

    void Start()
    {
        targetImage.sprite = buttonOnSprite;
    }

    public void OnClick()
    {
        if (isOn)
        {
            isOn = false;
            targetImage.sprite = buttonOffSprite;
        }
        else
        {
            isOn = true;
            targetImage.sprite = buttonOnSprite;
        }
    }
}
