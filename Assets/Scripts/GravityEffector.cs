using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffector : MonoBehaviour
{
    InputHandler inputHandler;
    [SerializeField] Transform targetTrans;
    [SerializeField] bool isEnabled;
    [SerializeField] float gravity;

    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
    }

    void Update()
    {
        if (isEnabled)
        {
            targetTrans.position -= Vector3.up * gravity * Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Ground))
        {
            isEnabled = false;
            inputHandler.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Ground))
        {
            isEnabled = true;
            inputHandler.SetActive(false);
        }
    }
}
