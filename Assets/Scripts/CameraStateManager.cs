using System.Collections;
using UnityEngine;

public enum CameraState
{
    Idle, Follow, Rotate
}

public class CameraStateManager : MonoBehaviour
{
    [SerializeField] Transform camTrans;
    [SerializeField] RotateObject rotateObject;
    [SerializeField] Vector3[] camPositions;
    [SerializeField] Quaternion[] camRotation;
    [SerializeField] float transitionTime;

    public void ChangeState(CameraState _state)
    {
        int index = (int)_state;
        switch (index)
        {
            case 0:
            case 1:
                StartCoroutine(CR_ChangeState(index));
                break;
            case 2:
                rotateObject.Rotate();
                break;
        }
    }

    IEnumerator CR_ChangeState(int _index)
    {
        float tick = 0;
        Vector3 startPosition = camTrans.localPosition;
        Vector3 endPosition = camPositions[_index];

        Quaternion startRotation = camTrans.localRotation;
        Quaternion endRotation = camRotation[_index];
        while (tick < transitionTime)
        {
            tick += Time.deltaTime;
            camTrans.localPosition = Vector3.Lerp(startPosition, endPosition, tick / transitionTime);
            camTrans.localRotation = Quaternion.Lerp(startRotation, endRotation, tick / transitionTime);
            yield return null;
        }
    }
}
