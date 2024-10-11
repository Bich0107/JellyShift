using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pagination : MonoBehaviour
{
    [SerializeField] GameObject pagePointPrefab;
    PagePoint[] pagePointScripts;
    int currentPageIndex = 0;

    public void Setup(int _pageAmount)
    {
        pagePointScripts = new PagePoint[_pageAmount];

        for (int i = 0; i < _pageAmount; i++)
        {
            GameObject g = Instantiate(pagePointPrefab, transform);
            pagePointScripts[i] = g.GetComponent<PagePoint>();
            pagePointScripts[i].SetActive(false);
        }

        pagePointScripts[currentPageIndex].SetActive(true);
    }

    public void ChangePage(int _index)
    {
        pagePointScripts[currentPageIndex].SetActive(false);
        pagePointScripts[_index].SetActive(true);
        currentPageIndex = _index;
    }
}
