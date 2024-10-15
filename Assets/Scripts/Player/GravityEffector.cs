using UnityEngine;

public class GravityEffector : MonoBehaviour
{
    [SerializeField] Follower camFollower;
    [SerializeField] Transform targetTrans;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] Transform boxTransform;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckBoxHeight = 0.2f;
    [SerializeField] float gravity;
    [Space]
    [SerializeField] bool isEnabled;
    Vector3 boxHalfScale;
    bool onGround = true;

    void Update()
    {
        GroundCheck();

        if (isEnabled)
        {
            targetTrans.position -= Vector3.up * gravity * Time.deltaTime;
        }
    }

    void GroundCheck()
    {
        boxHalfScale = boxTransform.localScale / 2f;
        boxHalfScale.y = groundCheckBoxHeight;

        onGround = Physics.CheckBox(groundCheckPos.position, boxHalfScale, targetTrans.localRotation, groundLayer, QueryTriggerInteraction.Collide);
        if (onGround)
        {
            isEnabled = false;
        }
        else
        {
            isEnabled = true;
            camFollower.Stop();
            GameManager.Instance.GameOver();
        }
    }

    // for debug
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Save the current Gizmos matrix
        Matrix4x4 oldMatrix = Gizmos.matrix;

        // Apply the object's position and rotation to Gizmos matrix
        Gizmos.matrix = Matrix4x4.TRS(groundCheckPos.position, targetTrans.localRotation, Vector3.one);

        // Draw the wireframe cube with the box extents
        Gizmos.DrawWireCube(Vector3.zero, boxHalfScale * 2);

        // Restore the original Gizmos matrix
        Gizmos.matrix = oldMatrix;
    }

    public bool OnGround => onGround;

    public void Reset()
    {
        isEnabled = false;
        onGround = true;
    }
}
