using UnityEngine;

public class ResetTransformOnDeactive : MonoBehaviour
{
    Vector3 localPosition;
    Quaternion localRotation;

    void OnEnable()
    {
        localPosition = transform.localPosition;
        localRotation = transform.localRotation;
    }

    void OnDisable()
    {
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }
}
