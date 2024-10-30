using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinChanger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] MeshRenderer[] skinRenderers;

    public void ChangeSkin(PlayerSkinSO _skin)
    {
        if (player.Skin != null)
        {
            player.Skin.IsChoosen = false;
            player.Skin.SaveStatus();
        }

        player.Skin = _skin;
        _skin.IsChoosen = true;
        _skin.SaveStatus();

        for (int i = 0; i < skinRenderers.Length; i++)
        {
            skinRenderers[i].material = _skin.SkinMaterial;
        }
    }
}
