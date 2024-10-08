using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverSystem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] CameraHelper cameraHelper;
    [SerializeField] MovingObject movingObject;
    [SerializeField] GameObject fever;
    [SerializeField] Slider feverBar;
    [SerializeField] Image barFilling;
    [Header("Camera settings")]
    [SerializeField] float feverFOV;
    [SerializeField] float normalFOV;
    [SerializeField] float fovChangeTime;
    [Header("Color settings")]
    [SerializeField] Color normalColor;
    [SerializeField] Color activeColor;
    [Header("Value settings")]
    [SerializeField] float maxFever = 1f;
    [SerializeField] float feverValue = 0f;
    [SerializeField] float feverPerObstacle = 0.2f;
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
        normalFOV = Camera.main.fieldOfView;
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

        feverValue = Mathf.Min(feverValue + feverPerObstacle, maxFever);
        if (feverValue >= maxFever) ActivateFever();
    }

    public void ReduceFever()
    {
        if (isActive) return;

        feverValue = Mathf.Max(feverValue - feverPerObstacle, 0f);
    }

    void ActivateFever()
    {
        isActive = true;
        normalSpeed = movingObject.CurrentSpeed;
        movingObject.CurrentSpeed = inFeverSpeed;
        barFilling.color = activeColor;

        cameraHelper.ChangeFOVOverTime(feverFOV, fovChangeTime);

        StartCoroutine(CR_ReduceFeverWhenActive());
    }

    void DeactiveFever()
    {
        isActive = false;
        barFilling.color = normalColor;
        movingObject.CurrentSpeed = endFeverSpeed;
        movingObject.ChangeSpeedOvertime(normalSpeed, speedRestoreTime);
        cameraHelper.ChangeFOVOverTime(normalFOV, fovChangeTime);
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
        if (feverValue < Mathf.Epsilon)
        {
            fever.SetActive(false);
        }
        else
        {
            fever.SetActive(true);
            feverBar.value = feverValue;
        }
    }

    public void TurnOff()
    {
        DeactiveFever();

        feverValue = 0f;
        fever.SetActive(false);
    }
}
