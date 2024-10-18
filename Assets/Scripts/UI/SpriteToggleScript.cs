using UnityEngine;
using UnityEngine.UI;

public class SpriteToggleScript : MonoBehaviour
{
    [SerializeField] Image targetImage;
    [SerializeField] Sprite buttonOffSprite;
    [SerializeField] Sprite buttonOnSprite;
    bool isOn = true;

    public void SetStatus(bool _status)
    {
        isOn = _status;
        targetImage.sprite = _status ? buttonOnSprite : buttonOffSprite;
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
