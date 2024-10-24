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
    [SerializeField] GameObject feverPassingVFX;
    [SerializeField] GameObject scoreDisplayerGO;
    [SerializeField] float expandTime;
    [SerializeField] Vector3 endScale;
    PassingScoreDisplayer scoreDisplayer;
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
        scoreDisplayer = scoreDisplayerGO.GetComponent<PassingScoreDisplayer>();
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

        VibrateManager.Instance.Vibrate();

        scoreDisplayerGO.SetActive(true);
        scoreDisplayer.Display();

        feverSystem.IncreaseFever();
        if (feverSystem.IsActive)
        {
            BodyExplode();
            SpawnVFX();
        }
        else
        {
            coverImage.color = player.Skin.PassingObstacleCoverColor;
            obstacleCoverTrans.gameObject.SetActive(true);
            StartCoroutine(CR_ExpandCover());
        }
    }

    void DisableBody()
    {
        for (int i = 0; i < bodyCubes.Length; i++)
        {
            bodyCubes[i].Disable();
        }
    }

    void BodyExplode()
    {
        for (int i = 0; i < bodyCubes.Length; i++)
        {
            bodyCubes[i].Break();
        }
    }

    void SpawnVFX()
    {
        GameObject vfx = ObjectPool.Instance.Spawn(feverPassingVFX.tag);
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;
        vfx.SetActive(true);
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
