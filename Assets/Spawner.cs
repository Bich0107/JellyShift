using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    int GetRandomNum(int _maxExcluded)
    {
        return Random.Range(0, _maxExcluded);
    }

    void Spawn(Vector3 _position, Quaternion _rotation, GameObject g)
    {
        g.transform.position = _position;
        g.transform.localRotation = _rotation;
        g.SetActive(true);
    }

    public void SpawnMultiple(Vector3 _spawnPos, Vector3[] _offsets, Quaternion _rotation)
    {
        GameObject randomPrefab = prefabs[GetRandomNum(prefabs.Length)];
        int randomSpawnTime = GetRandomNum(_offsets.Length);

        for (int i = 0; i < randomSpawnTime; i++)
        {
            GameObject g = ObjectPool.Instance.Spawn(randomPrefab.tag);
            Spawn(_spawnPos + _rotation * _offsets[i], _rotation, g);
        }
    }
}
