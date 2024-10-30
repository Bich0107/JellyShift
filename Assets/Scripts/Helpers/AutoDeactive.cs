using System.Collections;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
    [SerializeField] float delay;
    WaitForSecondsRealtime delayWait;

    void Awake()
    {
        delayWait = new WaitForSecondsRealtime(delay);
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(CR_AutoDeactive());
    }

    IEnumerator CR_AutoDeactive()
    {
        yield return delayWait;
        gameObject.SetActive(false);
    }
}
