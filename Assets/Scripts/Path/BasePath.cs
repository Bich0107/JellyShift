using UnityEngine;

public class BasePath : MonoBehaviour
{
    [SerializeField] Vector3 endPosOffset;
    [SerializeField] PathDirection pathDirection;

    public Vector3 EndPosOffset => endPosOffset;
    public PathDirection PathDirection => pathDirection;
}
