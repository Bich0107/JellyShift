using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSorter : MonoBehaviour
{
    [SerializeField] Vector3 distanceToLastPos;
    Vector3 currentPosition = Vector3.zero;

    public void SortChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = currentPosition;
            currentPosition += distanceToLastPos;
        }
    }
}
