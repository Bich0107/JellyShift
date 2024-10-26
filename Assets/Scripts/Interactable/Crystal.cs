using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] GameObject body;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crystalCollectSFX;
    [SerializeField] GameObject idleVFX;
    [SerializeField] GameObject collideVFX;
    [SerializeField] float deactiveDelay;
    WaitForSeconds deactiveWait;
    bool isDeactivating;

    void Start()
    {
        deactiveWait = new WaitForSeconds(deactiveDelay);
    }

    public void TriggerByPlayer()
    {
        if (isDeactivating) return;

        body.SetActive(false);
        idleVFX.SetActive(false);
        collideVFX.SetActive(true);
        audioSource.PlayOneShot(crystalCollectSFX);
        Bank.Instance.AddCrystal();
        StartCoroutine(CR_Deactive());
    }

    IEnumerator CR_Deactive()
    {
        isDeactivating = true;
        yield return deactiveWait;
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        body.SetActive(true);
        idleVFX.SetActive(true);
        collideVFX.SetActive(false);
        isDeactivating = false;
    }
}
