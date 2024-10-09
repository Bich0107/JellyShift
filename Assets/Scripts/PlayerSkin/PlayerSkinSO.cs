using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player skin SO", menuName = "Player/Player skin")]
public class PlayerSkinSO : ScriptableObject
{
    [SerializeField] int index;
    [SerializeField] Mesh skinMesh;
    [SerializeField] Material skinMaterial;
    [SerializeField] Material predictionBoxMaterial;
    [SerializeField] Color predictionImageColor;
    [SerializeField] Color passingObstacleCoverColor;
    [SerializeField] bool activated;
    [SerializeField] bool choosen;

    public int Index => index;
    public Mesh SkinMesh => skinMesh;
    public Material SkinMaterial => skinMaterial;
    public Material PredictionBoxMaterial => predictionBoxMaterial;
    public Color PredictionImageColor => predictionImageColor;
    public Color PassingObstacleCoverColor => passingObstacleCoverColor;
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
