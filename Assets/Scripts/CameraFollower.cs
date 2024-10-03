using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [Range(0.01f, 1f)]
    [SerializeField] float smoothFactor = 0.5f;
    Vector3 newPos;
    bool isFollowing = true;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (!isFollowing) return;

        newPos = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    }

    public void Stop() => isFollowing = true;
}
