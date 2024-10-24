using UnityEngine;
using UnityEngine.UI;

public class SkinBox : MonoBehaviour
{
    PlayerSkinChanger skinChanger;
    SkinDisplayer skinDisplayer;
    Transform skinDisplayerParent;
    [SerializeField] GameObject skinReviewPrefab;
    [SerializeField] PlayerSkinSO skinSO;
    [SerializeField] GameObject borderDefault;
    [SerializeField] GameObject borderChoosen;
    [SerializeField] GameObject skinActiveImage;
    [SerializeField] GameObject skinGachaImage;
    [SerializeField] GameObject skinDefaultImage;
    [SerializeField] RawImage activeImage;
    static SkinBox s_choosenSkin;
    public PlayerSkinSO SkinSO => skinSO;

    void Awake()
    {
        skinDisplayerParent = GameObject.FindGameObjectWithTag(Tags.SkinDisplayerParent).transform;
        skinChanger = FindObjectOfType<PlayerSkinChanger>();
    }

    public void SetSkin(PlayerSkinSO _skin)
    {
        skinSO = _skin;

        // find the current choosen skin and store it as a static variable, make sure there can only be one
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

        activeImage.texture = skinSO.ReviewRenderTexture;

        SetupSkinDisplayer();

        SetSkinboxUI(skinSO.IsActive, skinSO.IsChoosen);
    }

    void SetupSkinDisplayer()
    {
        GameObject g = Instantiate(skinReviewPrefab, skinDisplayerParent);
        skinDisplayer = g.GetComponent<SkinDisplayer>();
        skinDisplayer.SetSkin(skinSO.SkinMaterial, skinSO.ReviewRenderTexture);
    }

    public void OnSelect()
    {
        SelectSkin();
    }

    public void ActiveSkin()
    {
        if (!skinSO.IsActive)
        {
            SetSkinboxUI(true, false);
        }
    }

    void SelectSkin()
    {
        if (!skinSO.IsActive || skinSO.IsChoosen) return;

        skinChanger.ChangeSkin(skinSO);

        // update ui of previous choosen skin
        s_choosenSkin.SetSkinboxUI(true, false);

        SetSkinboxUI(true, true);
        s_choosenSkin = this;

        skinDisplayer.SelectSkin();
    }

    public void SetSkinboxUI(bool _isActive, bool _isChoosen)
    {
        skinActiveImage.SetActive(_isActive);
        skinDefaultImage.SetActive(!_isActive);
        skinSO.IsActive = _isActive;

        borderChoosen.SetActive(_isChoosen);
        borderDefault.SetActive(!_isChoosen);
        skinSO.IsChoosen = _isChoosen;
    }

    public void SetGachaImage(bool _status)
    {
        skinGachaImage.SetActive(_status);
        skinDefaultImage.SetActive(!_status);
    }
}