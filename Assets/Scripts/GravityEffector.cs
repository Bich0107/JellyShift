using UnityEngine;

public class GravityEffector : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] Transform raycastPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isEnabled;
    [SerializeField] float gravity;
    [SerializeField] float groundCheckDistance;
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
        Debug.DrawRay(raycastPos.position, Vector3.down * groundCheckDistance, Color.yellow);
        if (hit.collider == null)
        {
            isEnabled = true;
        }
        else
        {
            isEnabled = false;
        }
    }
}
