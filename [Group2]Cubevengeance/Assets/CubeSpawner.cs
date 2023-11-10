using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    //#1 ATTACH THIS SCRIPT TO SPAWNER
    [SerializeField] GameObject _prefabToSpawn;
    private int _spawnBounds = 100;


    void Start()
    {
        InvokeRepeating("SpawnPrefab", 1, 1);
    }

    void SpawnPrefab()
    {
        //RNG POS
        int x = Random.Range((int)this.transform.position.x - _spawnBounds, (int)this.transform.position.x + _spawnBounds);
        Vector3 spawnPos = new Vector3(x, this.transform.position.y, 0);

        //RNG ROT
        Quaternion rot = Quaternion.identity;
        rot.x = x;
        rot.y = Random.Range(-90, 90);
        rot.z = Random.Range(-90, 90);


        Instantiate(_prefabToSpawn, spawnPos, rot, this.transform);
    }
}