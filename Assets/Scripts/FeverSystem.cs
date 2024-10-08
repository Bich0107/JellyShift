using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverSystem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] MovingObject movingObject;
    [SerializeField] GameObject fever;
    [SerializeField] Slider feverBar;
    [SerializeField] Image barFilling;
    [Header("Color settings")]
    [SerializeField] Color normalColor;
    [SerializeField] Color activeColor;
    [Header("Value settings")]
    [SerializeField] float maxFever = 1f;
    [SerializeField] float feverValue = 0f;
    [SerializeField] float feverIncreasePerObstacle = 0.2f;
    [SerializeField] float feverDuration = 2f;
    [Header("Speed settings")]
    [SerializeField] float inFeverSpeed = 6f;
    [SerializeField] float endFeverSpeed = 1.5f;
    [SerializeField] float speedRestoreTime = 1.5f;
    float normalSpeed;
    bool isActive = false;
    public bool IsActive => isActive;

    void Start()
    {
        fever.SetActive(false);
        feverBar.maxValue = maxFever;
        barFilling.color = normalColor;
    }

    void Update()
    {
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
        normalSpeed = movingObject.CurrentSpeed;
        movingObject.CurrentSpeed = inFeverSpeed;
        barFilling.color = activeColor;

        StartCoroutine(CR_ReduceFeverWhenActive());
    }

    void DeactiveFever()
    {
        isActive = false;
        barFilling.color = normalColor;
        movingObject.CurrentSpeed = endFeverSpeed;
        StartCoroutine(movingObject.ChangeSpeedOvertime(normalSpeed, speedRestoreTime));
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

        DeactiveFever();
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
        DeactiveFever();

        feverValue = 0f;
        fever.SetActive(false);
    }
}
