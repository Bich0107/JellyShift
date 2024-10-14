using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player skin SO", menuName = "Player/Player skin")]
public class PlayerSkinSO : ScriptableObject
{
    [SerializeField] int index;
    [SerializeField] Material skinMaterial;
    [SerializeField] Material predictionBoxMaterial;
    [SerializeField] Color predictionImageColor;
    [SerializeField] Color passingObstacleCoverColor;
    [SerializeField] RenderTexture reviewRenderTexture;
    [SerializeField] bool activated;
    [SerializeField] bool choosen;

    public int Index => index;
    public Material SkinMaterial => skinMaterial;
    public Material PredictionBoxMaterial => predictionBoxMaterial;
    public Color PredictionImageColor => predictionImageColor;
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
}
