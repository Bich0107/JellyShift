using UnityEngine;

public class SkinPagesSetter : MonoBehaviour
{
    SkinBox[] skinBoxes;
    [SerializeField] BuyRandomSkinButton buyRandomSkinButton;
    [SerializeField] PositionSorter positionSorter;
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
        skinBoxes = new SkinBox[skins.Length];

        // calculate page amount based on skin amount
        pageAmount = skins.Length / skinPerPage + ((skins.Length % skinPerPage) > 0 ? 1 : 0);

        // create skin pages and its skin box
        for (int i = 0; i < pageAmount; i++)
        {
            pageGO = Instantiate(pageGrid, transform);

            for (int j = i * skinPerPage; j < i * skinPerPage + skinPerPage && j < skins.Length; j++)
            {
                skinGO = Instantiate(skinButton, pageGO.transform);
                SkinBox skinBoxScript = skinGO.GetComponent<SkinBox>();
                skinBoxScript.SetSkin(skins[j]);
                skinBoxes[j] = skinBoxScript;
            }
        }

        // change skin page with based on the amount of base and width per page
        float contentWidth = pageAmount * widthPerPage;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(contentWidth, rectTransform.sizeDelta.y);

        // after setting a skin, a skin displayer will be created and set to be child of the parent object,
        // position sorter is used to sort their position
        positionSorter.SortChildren();

        // setup skins on first page for buy button
        buyRandomSkinButton.SetSkinBoxes(GetSkinsOnPage(0));
    }

    public SkinBox[] GetSkinsOnPage(int _page)
    {
        SkinBox[] result = new SkinBox[skinPerPage];
        int index = 0;
        for (int j = _page * skinPerPage; j < _page * skinPerPage + skinPerPage && j < skins.Length; j++)
        {
            result[index] = skinBoxes[j];
            index++;
        }

        return result;
    }
}
