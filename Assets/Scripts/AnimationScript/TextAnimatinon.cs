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
            i %= texts.Length;
            text.text = texts[i];
            yield return delayWait;
            i++;
        } while (true);
    }
}
