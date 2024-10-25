using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crystalCollectSFX;
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
        isDeactivating = false;
    }
}
