using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverSystem : MonoBehaviour
{
    [SerializeField] GameObject fever;
    [SerializeField] Slider feverBar;
    [SerializeField] float maxFever = 1f;
    [SerializeField] float feverValue = 0f;
    [SerializeField] float feverIncreasePerObstacle = 0.25f;
    [SerializeField] float feverReduceRate = 0.05f;
    [SerializeField] float feverDuration = 2f;
    bool isActive = false;
    public bool IsActive => isActive;

    void Start()
    {
        fever.SetActive(false);
        feverBar.maxValue = maxFever;
    }

    void Update()
    {
        if (!isActive) ReduceFever();
        UpdateUI();
    }

    public void IncreaseFever()
    {
        if (isActive) return;

        feverValue = Mathf.Min(feverValue + feverIncreasePerObstacle, maxFever);
        if (feverValue >= maxFever) ActivateFever();
    }

    void ActivateFever()
    {
        isActive = true;

        StartCoroutine(CR_ReduceFeverWhenActive());
    }

    void ReduceFever()
    {
        feverValue = Mathf.Max(0f, feverValue - feverReduceRate * Time.deltaTime);
    }

    IEnumerator CR_ReduceFeverWhenActive()
    {
        float tick = 0;
        float startValue = maxFever;
        while (tick < feverDuration)
        {
            tick += Time.deltaTime;
            feverValue = Mathf.Lerp(startValue, 0f, tick / feverDuration);
            yield return null;
        }
        isActive = false;
    }

    void UpdateUI()
    {
        if (feverValue > 0f)
        {
            fever.SetActive(true);
            feverBar.value = feverValue;
        }
        else fever.SetActive(false);
    }

    public void TurnOff()
    {
        feverValue = 0f;
        isActive = false;
        fever.SetActive(false);
    }
}
