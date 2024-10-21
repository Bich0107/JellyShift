using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassingScoreDisplayer : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Vector3 moveOffset;
    [SerializeField] Vector3 endScale;
    [SerializeField] float duration;
    int score;

    void OnEnable()
    {
        score = LevelManager.Instance.CurrentSetting.ScorePerObstacle;
    }

    public void Display()
    {
        scoreText.text = score.ToString();
        PlayerScoreHandler.Instance.IncreaseScore(score);
        StartCoroutine(CR_DisplayAnimation());
    }

    IEnumerator CR_DisplayAnimation()
    {
        float tick = 0f;
        Vector3 startScale = targetTrans.localScale;
        Vector3 startPos = targetTrans.localPosition;
        Vector3 endPos = startPos + moveOffset;
        while (tick < duration)
        {
            tick += Time.deltaTime;
            targetTrans.localScale = Vector3.Lerp(startScale, endScale, tick / duration);
            targetTrans.localPosition = Vector3.Lerp(startPos, endPos, tick / duration);
            yield return null;
        }
        targetTrans.gameObject.SetActive(false);

    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
