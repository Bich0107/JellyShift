using UnityEngine;

public class BasePath : MonoBehaviour
{
    [SerializeField] Vector3 endPosOffset;
    [SerializeField] Vector3[] spawnPosOffsets;
    [SerializeField] PathDirection pathDirection;
    [SerializeField] float length;

    public Vector3 EndPosOffset => endPosOffset;
    public Vector3[] SpawnPosOffsets => spawnPosOffsets;
    public PathDirection PathDirection => pathDirection;
    public float Length => length;
}
