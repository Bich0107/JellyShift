using UnityEngine;

public class GroundPath : MonoBehaviour
{
    [SerializeField] Vector3 endPosOffset;
    [SerializeField] Vector3 direction;

    public Vector3 EndPosOffset => endPosOffset;
    public Vector3 Direction => direction;
}
