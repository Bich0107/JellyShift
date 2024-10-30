using System.Collections;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
    [SerializeField] float delay;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(CR_AutoDeactive());
    }

    IEnumerator CR_AutoDeactive()
    {
        yield return new WaitForSecondsRealtime(delay);
        gameObject.SetActive(false);
    }
}
