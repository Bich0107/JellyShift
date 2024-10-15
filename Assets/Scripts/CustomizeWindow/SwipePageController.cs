using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipePageController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] BuyRandomSkinButton buyRandomSkinButton;
    [SerializeField] SkinPagesSetter skinPagesSetter;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Pagination pagination;
    [SerializeField] GameObject contentHolder;
    [SerializeField] float transitionTime;
    [SerializeField] float minDragDistance = 0.1f;
    int pageAmount;
    int pageIndex = 0;
    int newPageIndex = 0;
    float dragStartPos;
    float[] pagePositions;
    bool isChangingPage;

    void Start()
    {
        pageAmount = contentHolder.transform.childCount;

        // set position for each pages
        pagePositions = new float[pageAmount];
        for (int i = 0; i < pageAmount; i++)
        {
            pagePositions[i] = i * (1f / (pageAmount - 1));
        }

        pagination.Setup(pageAmount);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        dragStartPos = scrollRect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (isChangingPage) return; // if transition is not finished yet, return

        float dragDistance = scrollRect.horizontalNormalizedPosition - dragStartPos;

        if (Mathf.Abs(dragDistance) > minDragDistance)
        {
            // check drag direction to change page
            if (dragDistance < Mathf.Epsilon)
            {
                newPageIndex = Mathf.Max(pageIndex - 1, 0);
            }
            else
            {
                newPageIndex = Mathf.Min(pageIndex + 1, pageAmount - 1);
            }
        }
        else
        {
            newPageIndex = pageIndex;
        }

        // check index and start transition
        StartCoroutine(CR_ChangePage(newPageIndex));

    }

    IEnumerator CR_ChangePage(int _newIndex)
    {
        isChangingPage = true;
        float tick = 0f;

        float startValue = scrollRect.horizontalNormalizedPosition;
        float endValue = pagePositions[_newIndex];
        while (tick < transitionTime)
        {
            tick += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startValue, endValue, tick / transitionTime);
            yield return null;
        }

        pageIndex = newPageIndex;
        pagination.ChangePage(pageIndex);

        // update current display skins for buy button
        buyRandomSkinButton.SetSkinBoxes(skinPagesSetter.GetSkinsOnPage(pageIndex));

        isChangingPage = false;
    }
}
