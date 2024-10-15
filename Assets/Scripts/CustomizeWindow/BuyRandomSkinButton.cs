using System.Collections;
using UnityEngine;

public class BuyRandomSkinButton : MonoBehaviour
{
    SkinBox[] skinBoxes;
    [SerializeField] Bank bank;
    [SerializeField] int pricePerSkin;
    [SerializeField] float loopInterval;
    [SerializeField] int minLoopTime = 12;
    [SerializeField] int maxLoopTIme = 18;
    WaitForSeconds loopWait;
    bool isSelecting;

    void Start()
    {
        loopWait = new WaitForSeconds(loopInterval);
    }

    public void SetSkinBoxes(SkinBox[] _skinBoxes)
    {
        skinBoxes = new SkinBox[_skinBoxes.Length];
        skinBoxes = _skinBoxes;
    }

    int GetRandomLoopTime()
    {
        return Random.Range(minLoopTime, maxLoopTIme + 1);
    }

    int CountInactiveSkin()
    {
        int counter = 0;
        for (int i = 0; i < skinBoxes.Length; i++)
        {
            if (!skinBoxes[i].SkinSO.IsActive)
            {
                counter++;
            }
        }
        return counter;
    }

    public void BuySkin()
    {
        if (isSelecting || !bank.CheckAmount(pricePerSkin) || CountInactiveSkin() == 0) return;

        // if therer is only 1 inactive skin, find and active it
        if (CountInactiveSkin() == 1)
        {
            for (int i = 0; i < skinBoxes.Length; i++)
            {
                if (!skinBoxes[i].SkinSO.IsActive)
                {
                    skinBoxes[i].ActiveSkin();
                    bank.TakeCrystal(pricePerSkin);
                    return;
                }
            }
        }

        StartCoroutine(CR_SelectSkin());
    }

    IEnumerator CR_SelectSkin()
    {
        isSelecting = true;

        int loopTime = GetRandomLoopTime();
        int counter = 0;

        // loop through inactive skin and change their ui for loopTime time
        do
        {
            for (int i = 0; i < skinBoxes.Length; i++)
            {
                if (!skinBoxes[i].SkinSO.IsActive)
                {
                    counter++;

                    if (counter >= loopTime)
                    {
                        skinBoxes[i].ActiveSkin();
                        bank.TakeCrystal(pricePerSkin);
                        isSelecting = false;
                        yield break;
                    }

                    skinBoxes[i].SetGachaImage(true);
                    yield return loopWait;
                    skinBoxes[i].SetGachaImage(false);
                }
            }
        } while (counter < loopTime);

        isSelecting = false;
    }

}
