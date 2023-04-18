using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerGiraffeNeckMesh : MonoBehaviour
{
    [SerializeField] float _spawnInterval;
    [SerializeField] Spawner _spawner;
    private float _elapsedTime = 0;

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _spawnInterval)
        {
            if (_spawner) _spawner.Spawn();
            _elapsedTime = 0;
        }
    }
}
