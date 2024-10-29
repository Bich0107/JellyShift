using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] GameObject body;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crystalCollectSFX;
    [SerializeField] GameObject idleVFX;
    [SerializeField] GameObject collideVFX;
    LifeHandler lifeHandler;
    [Space]
    [SerializeField] float deactiveDelay;
    [SerializeField] int crytalPerLife = 10;
    int counter = 0;
    WaitForSeconds deactiveWait;
    bool isDeactivating;

    void Awake()
    {
        deactiveWait = new WaitForSeconds(deactiveDelay);
        lifeHandler = FindObjectOfType<LifeHandler>();
    }

    public void TriggerByPlayer()
    {
        if (isDeactivating) return;

        counter++;
        if (counter >= crytalPerLife)
        {
            counter = 0;
            lifeHandler.IncreaseLife(1);
        }

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
