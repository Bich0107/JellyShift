using UnityEngine;

public class BasePath : MonoBehaviour
{
    [SerializeField] Vector3 endPosOffset;
    [SerializeField] Vector3[] obstaclePosOffsets;
    [SerializeField] PathDirection pathDirection;

    public Vector3 EndPosOffset => endPosOffset;
    public Vector3[] ObstaclePosOffsets => obstaclePosOffsets;
    public PathDirection PathDirection => pathDirection;
}
