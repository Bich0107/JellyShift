using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : MonoBehaviour
{
    [SerializeField] PlayerPassingHandler passingHandler;
    [SerializeField] float breakForce;
    [SerializeField] Vector3 forceDirection;
    [SerializeField] float deactiveDelay;
    WaitForSeconds deactiveWait;
    Rigidbody rb;
    bool isBroken;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        deactiveWait = new WaitForSeconds(deactiveDelay);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        ITriggerByObstacle hit = other.GetComponentInParent<ITriggerByObstacle>();
        if (hit != null)
        {
            hit.TriggerByObstacle();
            Break();
        }
    }

    public void Break()
    {
        if (isBroken) return;

        // turn on physics and shoot the cube away base on player position
        rb.isKinematic = false;
        rb.useGravity = true;

        forceDirection = (transform.position - passingHandler.PlayerTrans.position).normalized;
        rb.AddForce(forceDirection * breakForce, ForceMode.Impulse);

        isBroken = true;
        StartCoroutine(CR_Deactive());
    }

    IEnumerator CR_Deactive()
    {
        yield return deactiveWait;
        gameObject.SetActive(false);
    }
}
