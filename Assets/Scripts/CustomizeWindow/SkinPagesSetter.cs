using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPagesSetter : MonoBehaviour
{
    [SerializeField] PlayerSkinSO[] skins;
    [SerializeField] GameObject skinButton;
    [SerializeField] GameObject pageGrid;
    [SerializeField] int skinPerPage;
    [SerializeField] float widthPerPage;
    int pageAmount;
    GameObject skinGO;
    GameObject pageGO;

    void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        pageAmount = skins.Length / skinPerPage + ((skins.Length % skinPerPage) > 0 ? 1 : 0);

        for (int i = 0; i < pageAmount; i++)
        {
            pageGO = Instantiate(pageGrid, transform);

            for (int j = i * skinPerPage; j < i * skinPerPage + skinPerPage && j < skins.Length; j++)
            {
                skinGO = Instantiate(skinButton, pageGO.transform);
                skinGO.GetComponent<SkinBox>().SetSkin(skins[j]);
            }
        }

        float contentWidth = pageAmount * widthPerPage;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(contentWidth, rectTransform.sizeDelta.y);
    }
}
