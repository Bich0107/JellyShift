using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathDirection
{
    Forward, Left, Right
}

public class PathGenerator : MonoBehaviour
{
    [Header("Spawn amount and number of turn settings")]
    [SerializeField] DistanceBar playerDistanceBar;
    [SerializeField] int pathAmount = 10;
    int pathPerSegment;
    [Range(0, 2)]
    [SerializeField] int maxTurnTime;
    [SerializeField] bool randomTurnTime;
    [Tooltip("If the number of turn is >= 1, this is the chance for the first turn to be left")]
    [SerializeField] int turnLeftFirstChance = 50;
    int turnTime;
    [Header("Spawn path settings")]
    [SerializeField] Vector3 defaultSpawnPos;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] GameObject[] pathPrefabs;
    [SerializeField] GameObject[] leftTurnPrefabs;
    [SerializeField] GameObject[] rightTurnPrefabs;
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject goal;
    [SerializeField] Spawner spawner;
    float length;
    public float Length => length;
    GameObject firstTurnPath, secondTurnPath;
    Quaternion pathRotation;
    PathDirection lastDirection = PathDirection.Forward;

    void Start()
    {
        Reset();

        GeneratePaths();
    }

    void GetRandomTurnTime() => turnTime = Random.Range(0, maxTurnTime + 1);

    public void GeneratePaths()
    {
        length = 0;

        // calculate the amount of path per segment
        pathPerSegment = pathAmount / (turnTime + 1);

        // spawn start zone
        SetPath(startPos);

        // spawn paths forward
        GameObject pathPrefab = pathPrefabs[0];
        for (int i = 0; i < pathPerSegment; i++) SetPath(pathPrefab);

        // check turn time value and choose randomly to turn left-right or right-left
        if (turnTime > 0)
        {
            int rand = Random.Range(0, 100);
            if (rand > turnLeftFirstChance)
            {
                firstTurnPath = GetRandomGOFromArray(leftTurnPrefabs);
                secondTurnPath = GetRandomGOFromArray(rightTurnPrefabs);
            }
            else
            {
                firstTurnPath = GetRandomGOFromArray(rightTurnPrefabs);
                secondTurnPath = GetRandomGOFromArray(leftTurnPrefabs);
            }

            // spawn the first turn and the paths follow
            if (turnTime >= 1)
            {
                SetPath(firstTurnPath);
                pathPrefab = pathPrefabs[0];
                for (int i = 0; i < pathPerSegment; i++)
                {
                    SetPath(pathPrefab);
                }
            }

            // spawn the second turn and the paths follow
            if (turnTime >= 2)
            {
                SetPath(secondTurnPath);
                // select & spawn path forward
                pathPrefab = pathPrefabs[0];
                for (int i = 0; i < pathPerSegment; i++)
                {
                    SetPath(pathPrefab);
                }
            }
        }

        // spawn goal
        SetPath(goal);

        // set final distance for the progress bar
        playerDistanceBar.SetMaxDistance(length);
    }

    GameObject GetRandomGOFromArray(GameObject[] _array)
    {
        int rand = Random.Range(0, _array.Length);
        return _array[rand];
    }

    void SetPath(GameObject _path)
    {
        GameObject g = ObjectPool.Instance.Spawn(_path.tag);
        g.transform.position = spawnPos;
        g.SetActive(true);

        // find the rotation of the current path base on last path's direction
        BasePath pathScript = g.GetComponent<BasePath>();
        switch (lastDirection)
        {
            case PathDirection.Left:
                pathRotation *= Quaternion.AngleAxis(-90f, Vector3.up);
                break;
            case PathDirection.Right:
                pathRotation *= Quaternion.AngleAxis(90f, Vector3.up);
                break;
            case PathDirection.Forward:
                pathRotation *= Quaternion.identity;
                break;
        }
        lastDirection = pathScript.PathDirection;

        // calculte the total length of the path
        length += pathScript.Length;

        // spawn obstacle and/or crystal on every possible position on the path
        if (pathScript.SpawnPosOffsets.Length > 0)
        {
            spawner.Spawn(spawnPos, pathScript.SpawnPosOffsets, pathRotation);
        }

        // set rotation and update spawn pos
        g.transform.localRotation = pathRotation;
        spawnPos += pathRotation * pathScript.EndPosOffset;
    }

    public void Reset()
    {
        spawnPos = defaultSpawnPos;
        pathRotation = Quaternion.identity;

        if (randomTurnTime) GetRandomTurnTime();
        else turnTime = maxTurnTime;
    }
}
