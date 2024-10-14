using UnityEngine;

public class SkinPagesSetter : MonoBehaviour
{
    [SerializeField] BuyRandomSkinButton buyRandomSkinButton;
    [SerializeField] PositionSorter positionSorter;
    [SerializeField] PlayerSkinSO[] skins;
    [SerializeField] SkinBox[] skinBoxes;
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
        // calculate page amount based on skin amount
        pageAmount = skins.Length / skinPerPage + ((skins.Length % skinPerPage) > 0 ? 1 : 0);

        skinBoxes = new SkinBox[skins.Length];

        // create skin pages and its skin box
        for (int i = 0; i < pageAmount; i++)
        {
            pageGO = Instantiate(pageGrid, transform);

            for (int j = i * skinPerPage; j < i * skinPerPage + skinPerPage && j < skins.Length; j++)
            {
                skinGO = Instantiate(skinButton, pageGO.transform);
                SkinBox script = skinGO.GetComponent<SkinBox>();
                script.SetSkin(skins[j]);
                skinBoxes[j] = script;
            }
        }

        // change skin page with based on the amount of base and width per page
        float contentWidth = pageAmount * widthPerPage;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(contentWidth, rectTransform.sizeDelta.y);

        // after setting a skin, a skin displayer will be created and set to be child of the parent object,
        // position sorter is used to sort their position
        positionSorter.SortChildren();

        // pass skinboxes on the first page to buy button
        buyRandomSkinButton.Setup(GetSkinOnPage(0));
    }

    public SkinBox[] GetSkinOnPage(int _pageIndex)
    {
        int index = 0;
        SkinBox[] result = new SkinBox[skinPerPage];

        for (int j = _pageIndex * skinPerPage; j < _pageIndex * skinPerPage + skinPerPage && j < skins.Length; j++)
        {
            result[index] = skinBoxes[j];
            index++;
        }

        return result;
    }
}
