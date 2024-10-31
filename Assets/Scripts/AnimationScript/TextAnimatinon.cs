using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string[] texts;
    [SerializeField] float delay;
    [SerializeField] bool playOnEnable;
    WaitForSecondsRealtime delayWait;

    void Awake()
    {
        delayWait = new WaitForSecondsRealtime(delay);
    }

    void OnEnable()
    {
        StopAllCoroutines();
        text.text = texts[0];
        StartCoroutine(CR_IdleAnimation());
    }

    IEnumerator CR_IdleAnimation()
    {
        int i = 1;
        do
        {
            yield return delayWait;
            i = i >= texts.Length ? 0 : i;
            text.text = texts[i];
            i++;
        } while (true);
    }
}
