using System.Collections;
using UnityEngine;

public class PlayerPassingHandler : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] Transform playerTrans;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] Transform obstacleCoverTrans;
    [SerializeField] ObstacleCube[] bodyCubes;
    [SerializeField] float expandTime;
    [SerializeField] Vector3 endScale;
    public Transform PlayerTrans => playerTrans;
    bool isTrigger = false;

    void Start()
    {
        playerTrans = FindObjectOfType<InputHandler>().transform;
        feverSystem = FindObjectOfType<FeverSystem>();
    }

    public void TriggerByPlayer()
    {
        if (isTrigger) return;

        isTrigger = true;

        feverSystem.IncreaseFever();
        if (feverSystem.IsActive)
        {
            BodyExplode();
        }

        obstacleCoverTrans.gameObject.SetActive(true);
        StartCoroutine(CR_ExpandCover());
    }

    void BodyExplode()
    {
        for (int i = 0; i < bodyCubes.Length; i++)
        {
            bodyCubes[i].Break();
        }
    }

    IEnumerator CR_ExpandCover()
    {
        float tick = 0f;
        Vector3 baseScale = obstacleCoverTrans.localScale;
        while (tick < expandTime)
        {
            tick += Time.deltaTime;
            obstacleCoverTrans.localScale = Vector3.Lerp(baseScale, endScale, tick / expandTime);
            yield return null;
        }

        obstacleCoverTrans.gameObject.SetActive(false);
    }
}
