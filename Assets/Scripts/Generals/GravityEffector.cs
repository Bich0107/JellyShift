using UnityEngine;

public class GravityEffector : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] Transform raycastPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isEnabled;
    [SerializeField] float gravity;
    [SerializeField] float groundCheckDistance;
    bool onGround = true;
    RaycastHit hit;

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
        Physics.Raycast(raycastPos.position, Vector3.down, out hit, groundCheckDistance, groundLayer, QueryTriggerInteraction.Collide);
        if (hit.collider == null)
        {
            isEnabled = true;
            onGround = false;
        }
        else
        {
            onGround = true;
            isEnabled = false;
        }
    }

    public bool OnGround => onGround;
}
