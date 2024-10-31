using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player skin SO", menuName = "Player/Player skin")]
public class PlayerSkinSO : ScriptableObject
{
    [SerializeField] int index;
    [SerializeField] Material skinMaterial;
    [SerializeField] Color passingObstacleCoverColor;
    [SerializeField] RenderTexture reviewRenderTexture;
    [SerializeField] bool activated;
    [SerializeField] bool choosen;

    public int Index => index;
    public Material SkinMaterial => skinMaterial;
    public Color PassingObstacleCoverColor => passingObstacleCoverColor;
    public RenderTexture ReviewRenderTexture => reviewRenderTexture;
    public bool IsActive
    {
        get { return activated; }
        set { activated = value; }
    }
    public bool IsChoosen
    {
        get { return choosen; }
        set { choosen = value; }
    }

    public void SaveStatus()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("Skin_" + Index, json);
    }

    public void LoadStatus()
    {
        string jsonData = PlayerPrefs.GetString("Skin_" + Index);

        if (!string.IsNullOrEmpty(jsonData))
        {
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }
}
