using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] GameObject adLoadFailedPanel;
    [SerializeField] Transform targetTrans;
    [SerializeField] float autoCloseDelay = 10f;
    WaitForSecondsRealtime autoCloseWait;

    void Awake()
    {
        autoCloseWait = new WaitForSecondsRealtime(autoCloseDelay);
    }

    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(CR_AutoClose());
        targetTrans.localScale = Vector3.one;
    }
    public void Close()
    {
        StopAllCoroutines();
        targetTrans.localScale = Vector3.zero;
    }

    IEnumerator CR_AutoClose()
    {
        yield return autoCloseWait;
        Close();
        adLoadFailedPanel.SetActive(true);
    }
}
