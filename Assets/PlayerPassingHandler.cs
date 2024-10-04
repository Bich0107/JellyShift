using System.Collections;
using UnityEngine;

public class PlayerPassingHandler : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] Transform obstacleCoverTrans;
    [SerializeField] float expandTime;
    [SerializeField] Vector3 endScale;
    bool isTrigger = false;

    public void TriggerByPlayer()
    {
        if (isTrigger) return;

        isTrigger = true;
        obstacleCoverTrans.gameObject.SetActive(true);
        StartCoroutine(CR_ExpandCover());
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
