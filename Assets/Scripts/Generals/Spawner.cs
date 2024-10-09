using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] GameObject crystalPrefab;
    [SerializeField] Vector3[] crystalOffsets;
    [Tooltip("This value determine how often obstacle is spawn, spawner will rest for spawnRestTime path(s) after spawning")]
    [Range(1, 3)]
    [SerializeField] int maxSpawnRestTime;
    [SerializeField] bool spawnObstacle = true;
    [SerializeField] bool spawnCrystal = true;
    [SerializeField] bool randomSpawnRestTime = true;
    [SerializeField] bool randomObstacleAmount = true;
    [SerializeField] bool randomCrystalAmount = false;
    GameObject temp;
    Vector3 position;
    int spawnTime;
    int restCounter = 0;

    int GetRandomNum(int _max, int _min = 0)
    {
        return Random.Range(_min, _max + 1);
    }

    void SetupGO(Vector3 _position, Quaternion _rotation, GameObject g)
    {
        g.transform.position = _position;
        g.transform.localRotation = _rotation;
        g.SetActive(true);
    }

    public void Spawn(Vector3 _spawnPos, Vector3[] _offsets, Quaternion _rotation)
    {
        int obstacleIndex = GetRandomNum(obstaclePrefabs.Length - 1);
        GameObject obstaclePrefab = obstaclePrefabs[obstacleIndex];

        if (restCounter <= 0 && spawnObstacle)
        {
            // get obstacle spawn amount (random or all)
            if (randomObstacleAmount)
                spawnTime = GetRandomNum(_offsets.Length, 1); // make sure spawn time is at least 1
            else spawnTime = _offsets.Length;

            // spawn obstacles
            for (int i = 0; i < spawnTime; i++)
            {
                temp = ObjectPool.Instance.Spawn(obstaclePrefab.tag);
                position = _spawnPos + _rotation * _offsets[i];
                SetupGO(position, _rotation, temp);
            }

            if (randomSpawnRestTime) restCounter = GetRandomNum(maxSpawnRestTime, 1);// make sure rest time is at least 1
            else restCounter = maxSpawnRestTime;
        }
        else restCounter--;

        if (spawnCrystal)
        {
            // get crystal spawn amount (random or all)
            if (randomCrystalAmount) spawnTime = GetRandomNum(_offsets.Length);
            else spawnTime = _offsets.Length;

            // spawn crystal
            for (int i = 0; i < spawnTime; i++)
            {
                temp = ObjectPool.Instance.Spawn(crystalPrefab.tag);
                position = _spawnPos + _rotation * (_offsets[i] + crystalOffsets[obstacleIndex]);
                SetupGO(position, _rotation, temp);
            }
        }
    }
}
