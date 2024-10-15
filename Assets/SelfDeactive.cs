using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeactive : MonoBehaviour
{
    [SerializeField] float lifetime;
    WaitForSeconds lifeWait;

    void Awake()
    {
        lifeWait = new WaitForSeconds(lifetime);
    }

    void OnEnable()
    {
        StartCoroutine(CR_SelfDeactive());
    }

    IEnumerator CR_SelfDeactive()
    {
        yield return lifeWait;
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
