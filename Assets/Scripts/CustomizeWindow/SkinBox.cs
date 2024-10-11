using UnityEngine;

public class SkinBox : MonoBehaviour
{
    PlayerSkinChanger skinChanger;
    [SerializeField] PlayerSkinSO skinSO;
    [SerializeField] GameObject borderDefault;
    [SerializeField] GameObject borderChoosen;
    [SerializeField] GameObject skinActiveImage;
    [SerializeField] GameObject skinDefaultImage;
    static SkinBox s_choosenSkin;

    void Awake()
    {
        skinChanger = FindObjectOfType<PlayerSkinChanger>();
    }

    public void SetSkin(PlayerSkinSO _skin)
    {
        skinSO = _skin;

        if (skinSO.IsChoosen)
        {
            if (s_choosenSkin == null)
            {
                s_choosenSkin = this;
            }
            else if (s_choosenSkin != this)
            {
                skinSO.IsChoosen = false;
            }
        }

        SetSkinboxUI(skinSO.IsActive, skinSO.IsChoosen);
    }

    public void OnSelect()
    {
        ActiveSkin();
        SelectSkin();
    }

    public void ActiveSkin()
    {
        if (!skinSO.IsActive)
        {
            skinSO.IsActive = true;
            SetSkinboxUI(true, false);
        }
    }

    void SelectSkin()
    {
        if (!skinSO.IsChoosen)
        {
            skinChanger.ChangeSkin(skinSO);
            s_choosenSkin.SetSkinboxUI(true, false);
            SetSkinboxUI(true, true);
            s_choosenSkin = this;
        }
    }

    public void SetSkinboxUI(bool _isActive, bool _isChoosen)
    {
        skinActiveImage.SetActive(_isActive);
        skinDefaultImage.SetActive(!_isActive);

        borderChoosen.SetActive(_isChoosen);
        borderDefault.SetActive(!_isChoosen);
    }
}
