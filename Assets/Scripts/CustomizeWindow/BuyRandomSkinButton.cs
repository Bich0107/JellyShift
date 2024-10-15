using System.Collections;
using UnityEngine;

public class BuyRandomSkinButton : MonoBehaviour
{
    SkinBox[] currentPageSkinBoxes;
    [SerializeField] Bank bank;
    [SerializeField] int pricePerSkin;
    [SerializeField] int minSelectTime;
    [SerializeField] int maxSelectTime;
    [SerializeField] float changeDelay;
    bool isSelecting;

    public void Setup(SkinBox[] _skinBoxes)
    {
        currentPageSkinBoxes = _skinBoxes;
    }

    int GetRandomSelectTime()
    {
        return Random.Range(minSelectTime, maxSelectTime + 1);
    }

    int CountInactiveSkin()
    {
        int result = 0;
        for (int i = 0; i < currentPageSkinBoxes.Length; i++)
        {
            if (!currentPageSkinBoxes[i].SkinSO.IsActive) result++;
        }

        return result;
    }

    public void OnClick()
    {
        if (isSelecting) return;

        if (!bank.CheckAmount(pricePerSkin))
        {
            Debug.Log("not enough crystal");
            // make some effect to notice player here
            return;
        }

        int counter = CountInactiveSkin();

        if (counter == 0)
        {
            Debug.Log("all skin is bought");
            return;
        }

        if (counter == 1)
        {
            for (int i = 0; i < currentPageSkinBoxes.Length; i++)
            {
                if (!currentPageSkinBoxes[i].SkinSO.IsActive)
                {
                    currentPageSkinBoxes[i].ActiveSkin();
                    bank.TakeCrystal(pricePerSkin);
                    return;
                }
            }
        }

        StartCoroutine(CR_SelectRandomSkin());
    }

    IEnumerator CR_SelectRandomSkin()
    {
        isSelecting = true;

        int selectTime = GetRandomSelectTime();
        int counter = -1;

        do
        {
            for (int i = 0; i < currentPageSkinBoxes.Length; i++)
            {
                // check if this skin box in inactive
                if (!currentPageSkinBoxes[i].SkinSO.IsActive)
                {
                    counter++;
                    // check counter to active inactive skin after looping enough time
                    if (counter >= selectTime)
                    {
                        currentPageSkinBoxes[i].ActiveSkin();
                        break;
                    }

                    // update ui of this skin box
                    currentPageSkinBoxes[i].SetGachaImage(true);
                    yield return new WaitForSeconds(changeDelay);
                    currentPageSkinBoxes[i].SetGachaImage(false);
                }
            }
            // loop for select time
        } while (counter < selectTime);

        bank.TakeCrystal(pricePerSkin);

        isSelecting = false;
    }
}
