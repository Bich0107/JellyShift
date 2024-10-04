using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : MonoBehaviour
{
    [SerializeField] GravityEffector gravityEffector;
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
            forceDirection = (transform.position - other.transform.position).normalized;
            hit.TriggerByObstacle();
            Break();
        }
    }

    public void Break()
    {
        if (isBroken) return;

        // shoot the cube away base on player collide position
        rb.isKinematic = false;
        rb.AddForce(forceDirection * breakForce, ForceMode.Impulse);

        // turn on gravity
        gravityEffector.enabled = true;
        isBroken = true;

        StartCoroutine(CR_Deactive());
    }

    IEnumerator CR_Deactive()
    {
        yield return deactiveWait;
        gameObject.SetActive(false);
    }
}
