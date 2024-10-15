using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPassingHandler : MonoBehaviour, ITriggerByPlayer
{
    [Header("Components")]
    [SerializeField] Transform obstacleCoverTrans;
    [SerializeField] ObstacleCube[] bodyCubes;
    [SerializeField] Image coverImage;
    [Header("Player passing settings")]
    [SerializeField] float expandTime;
    [SerializeField] Vector3 endScale;
    Vector3 baseScale;
    Player player;
    Transform playerTrans;
    public Transform PlayerTrans => playerTrans;
    bool isTrigger = false;
    FeverSystem feverSystem;

    void Start()
    {
        baseScale = obstacleCoverTrans.localScale;

        player = FindObjectOfType<Player>();
        playerTrans = player.gameObject.transform;
        feverSystem = FindObjectOfType<FeverSystem>();
    }

    void OnEnable()
    {
        for (int i = 0; i < bodyCubes.Length; i++)
        {
            bodyCubes[i].gameObject.SetActive(true);
        }
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
        else
        {
            coverImage.color = player.Skin.PassingObstacleCoverColor;
            obstacleCoverTrans.gameObject.SetActive(true);
            StartCoroutine(CR_ExpandCover());
        }
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
        while (tick < expandTime)
        {
            tick += Time.deltaTime;
            obstacleCoverTrans.localScale = Vector3.Lerp(baseScale, endScale, tick / expandTime);
            yield return null;
        }

        obstacleCoverTrans.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        if (obstacleCoverTrans == null) return;

        obstacleCoverTrans.localScale = baseScale;
        isTrigger = false;
    }
}
