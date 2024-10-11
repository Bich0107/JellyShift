using UnityEngine;

public class SkinBox : MonoBehaviour
{
    PlayerSkinChanger skinChanger;
    [SerializeField] PlayerSkinSO skinSO;

    void Awake()
    {
        skinChanger = FindObjectOfType<PlayerSkinChanger>();
    }

    public void SetSkin(PlayerSkinSO _skin)
    {
        skinSO = _skin;
    }

    public void OnSelect()
    {
        ActiveSkin();
        SelectSkin();
        Display();
    }

    void ActiveSkin()
    {
        if (!skinSO.IsActive)
        {
            skinSO.IsActive = true;
            Debug.Log($"Active skin {skinSO}");
        }
    }

    void SelectSkin()
    {
        if (!skinSO.IsChoosen)
        {
            skinChanger.ChangeSkin(skinSO);
            Debug.Log($"Active to skin {skinSO}");
        }
    }

    public void Display()
    {
        Debug.Log($"play some animation for {skinSO}");
    }
}
