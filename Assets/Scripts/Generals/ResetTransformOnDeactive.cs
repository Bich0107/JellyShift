using UnityEngine;

public class ResetTransformOnDeactive : MonoBehaviour
{
    Vector3 localPosition;
    Quaternion localRotation;

    void Awake()
    {
        localPosition = transform.localPosition;
        localRotation = transform.localRotation;
    }

    void OnDisable()
    {
        Reset();
    }

    public void Reset()
    {
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }
}
