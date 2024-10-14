using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDisplayer : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Animator animator;

    public void SetSkin(Material _skinMaterial, RenderTexture _renderTexture)
    {
        cam.targetTexture = _renderTexture;
        meshRenderer.material = _skinMaterial;
    }

    public void SelectSkin()
    {
        animator.SetTrigger("selected");
    }
}
