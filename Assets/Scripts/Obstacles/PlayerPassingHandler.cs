using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPassingHandler : MonoBehaviour, ITriggerByPlayer
{
    Player player;
    Transform playerTrans;
    FeverSystem feverSystem;
    [Header("Components")]
    [SerializeField] Transform obstacleCoverTrans;
    [SerializeField] ObstacleCube[] bodyCubes;
    [SerializeField] Image coverImage;
    [Header("Player passing settings")]
    [SerializeField] float expandTime;
    [SerializeField] Vector3 endScale;
    public Transform PlayerTrans => playerTrans;
    bool isTrigger = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerTrans = player.gameObject.transform;
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

        coverImage.color = player.Skin.PassingObstacleCoverColor;
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
