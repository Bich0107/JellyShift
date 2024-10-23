using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinChanger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] MeshRenderer[] skinRenderers;
    [SerializeField] MeshRenderer predictionBoxRenderer;
    [SerializeField] Image predictionImage;

    public void ChangeSkin(PlayerSkinSO _skin)
    {
        player.Skin.IsChoosen = false;
        player.Skin = _skin;
        _skin.IsChoosen = true;

        for (int i = 0; i < skinRenderers.Length; i++)
        {
            skinRenderers[i].material = _skin.SkinMaterial;
        }

        // predictionBoxRenderer.material = _skin.PredictionBoxMaterial;
        // predictionImage.color = _skin.PredictionImageColor;
    }
}
