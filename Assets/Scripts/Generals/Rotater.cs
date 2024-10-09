using System.Collections;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    Quaternion baseRotation;
    [SerializeField] Transform targetTrans;
    bool rotating;

    void Awake()
    {
        baseRotation = targetTrans.localRotation;
    }

    public void Rotate(Quaternion _rotation, float _duration)
    {
        if (rotating) return;

        StartCoroutine(CR_Rotate(_rotation, _duration));
    }

    IEnumerator CR_Rotate(Quaternion _rotation, float _duration)
    {
        rotating = true;

        float tick = 0f;
        Quaternion startRotation = targetTrans.localRotation;
        Quaternion endRotation = startRotation * _rotation;

        while (tick < _duration)
        {
            tick += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, tick / _duration);
            yield return null;
        }

        rotating = false;
    }

    public void Reset()
    {
        StopAllCoroutines();
        rotating = false;
        targetTrans.localRotation = baseRotation;
    }
}
