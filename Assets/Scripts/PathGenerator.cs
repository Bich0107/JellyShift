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
    [SerializeField] bool turnRoad = true;
    int leftChance = 50;

    void Start()
    {
        pathRotation = Quaternion.identity;
        GeneratePaths();
    }

    public void GeneratePaths()
    {
        // spawn start zone
        SetPath(startPos);

        // spawn paths forward
        GameObject pathPrefab = pathPrefabs[0];
        for (int i = 0; i < pathAmount; i++)
        {
            SetPath(pathPrefab);
        }

        if (turnRoad)
        {
            int rand = Random.Range(0, 100);
            if (rand < leftChance)
            {
                // select path to the left
                pathPrefab = cornerPrefabs[0];
            }
            else
            {
                // select path to the right
                pathPrefab = cornerPrefabs[1];
            }
            for (int i = 0; i < pathAmount; i++)
            {
                SetPath(pathPrefab);
            }
        }

        // select & spawn path forward
        pathPrefab = pathPrefabs[0];
        for (int i = 0; i < pathAmount; i++)
        {
            SetPath(pathPrefab);
        }

        // spawn start zone
        SetPath(goal);
    }

    void SetPath(GameObject _path)
    {
        GameObject g = Instantiate(_path, spawnPos, Quaternion.identity, transform);

        GroundPath pathScript = g.GetComponent<GroundPath>();
        //g.transform.localRotation = pathRotation;
        spawnPos += pathScript.EndPosOffset;
    }
}
