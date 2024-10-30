using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] GameObject adLoadFailedPanel;
    [SerializeField] Transform targetTrans;
    [SerializeField] float autoCloseDelay = 10f;

    public void Open()
    {
        StopAllCoroutines();

        adLoadFailedPanel.SetActive(false);
        StartCoroutine(CR_AutoClose());
        targetTrans.localScale = Vector3.one;
    }
    public void Close()
    {
        StopAllCoroutines();
        adLoadFailedPanel.SetActive(false);

        targetTrans.localScale = Vector3.zero;
    }

    IEnumerator CR_AutoClose()
    {
        yield return new WaitForSecondsRealtime(autoCloseDelay);
        Close();
        adLoadFailedPanel.SetActive(true);
    }
}
