using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SkinBox : MonoBehaviour
{
    [Header("Audio settings")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip selectSound;
    [Header("UI settings")]
    [SerializeField] GameObject skinReviewPrefab;
    [SerializeField] PlayerSkinSO skinSO;
    [SerializeField] GameObject borderDefault;
    [SerializeField] GameObject borderChoosen;
    [SerializeField] GameObject skinActiveImage;
    [SerializeField] GameObject skinDefaultImage;
    [SerializeField] GameObject skinGachaImage;
    [SerializeField] RawImage activeImage;
    PlayerSkinChanger skinChanger;
    SkinDisplayer skinDisplayer;
    Transform skinDisplayerParent;
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
        skinSO.LoadStatus();

        // find the current choosen skin and store it as a static variable, make sure there can only be one
        if (skinSO.IsChoosen)
        {
            if (s_choosenSkin == null)
            {
                s_choosenSkin = this;
                skinChanger.ChangeSkin(skinSO);
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

    // set status for skin box when using buy random skin button
    public void SetGachaImage(bool _status)
    {
        skinDefaultImage.SetActive(!_status);
        skinGachaImage.SetActive(_status);
    }

    void SetupSkinDisplayer()
    {
        GameObject g = Instantiate(skinReviewPrefab, skinDisplayerParent);
        skinDisplayer = g.GetComponent<SkinDisplayer>();
        skinDisplayer.SetSkin(skinSO.SkinMaterial, skinSO.ReviewRenderTexture);
    }

    public void OnSelect()
    {
        if (!skinSO.IsActive) return;

        if (!skinSO.IsChoosen)
        {
            audioSource.PlayOneShot(selectSound);

            skinChanger.ChangeSkin(skinSO);

            // update ui of previous choosen skin
            s_choosenSkin.SetSkinboxUI(true, false);

            SetSkinboxUI(true, true);
            s_choosenSkin = this;

            skinDisplayer.SelectSkin();
        }

        skinSO.SaveStatus();
    }

    public void ActiveSkin()
    {
        if (!skinSO.IsActive)
        {
            skinSO.IsActive = true;
            SetSkinboxUI(true, false);

            skinSO.SaveStatus();
        }
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
}
