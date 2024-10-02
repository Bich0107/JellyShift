using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] int pathAmount = 10;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Quaternion pathRotation;
    [SerializeField] GameObject[] pathPrefabs;
    [SerializeField] GameObject[] cornerPrefabs;
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject goal;

    void Start()
    {
        pathRotation = Quaternion.identity;
        GeneratePath();
    }

    public void GeneratePath()
    {
        for (int i = 0; i < pathAmount; i++)
        {
            GameObject g;
            g = Instantiate(startPos, spawnPos, Quaternion.identity, transform);

            GroundPath pathScript = g.GetComponent<GroundPath>();
            g.transform.localRotation = pathRotation;
            spawnPos += pathScript.EndPosOffset;
        }

        for (int i = 0; i < pathAmount; i++)
        {
            GameObject g;
            g = Instantiate(cornerPrefabs[0], spawnPos, Quaternion.identity, transform);

            GroundPath pathScript = g.GetComponent<GroundPath>();
            g.transform.localRotation = pathRotation;
            spawnPos += pathScript.EndPosOffset;
        }

        for (int i = 0; i < pathAmount; i++)
        {
            GameObject g;
            g = Instantiate(startPos, spawnPos, Quaternion.identity, transform);

            GroundPath pathScript = g.GetComponent<GroundPath>();
            g.transform.localRotation = pathRotation;
            spawnPos += pathScript.EndPosOffset;
        }

        for (int i = 0; i < pathAmount; i++)
        {
            GameObject g;
            g = Instantiate(cornerPrefabs[1], spawnPos, Quaternion.identity, transform);

            GroundPath pathScript = g.GetComponent<GroundPath>();
            g.transform.localRotation = pathRotation;
            spawnPos += pathScript.EndPosOffset;
        }
    }
}
