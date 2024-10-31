using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerByPlayer
{
    static int s_counter = 0;
    public static int s_crytalPerLife;
    public static int s_scoreIncrease;

    [SerializeField] GameObject body;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crystalCollectSFX;
    [SerializeField] AudioClip increaseLifeSFX;
    [SerializeField] GameObject idleVFX;
    [SerializeField] GameObject collideVFX;
    [Space]
    [SerializeField] float deactiveDelay;
    WaitForSeconds deactiveWait;
    bool isDeactivating;

    void Awake()
    {
        deactiveWait = new WaitForSeconds(deactiveDelay);
    }

    public void TriggerByPlayer()
    {
        if (isDeactivating) return;

        s_counter++;
        if (s_counter >= s_crytalPerLife)
        {
            s_counter = 0;
            LifeHandler.Instance.IncreaseLife(1);
            audioSource.PlayOneShot(increaseLifeSFX);
            PlayerScoreHandler.Instance.IncreaseScore(s_scoreIncrease);
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

    public static void ResetCounter() => s_counter = 0;
}
