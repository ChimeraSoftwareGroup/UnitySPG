using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] Spawner[] _spawners;
    [SerializeField] float _spawnInterval;
    private float _elapsedTime;
    private int _lastSpawn;
    private int _nowSpawn;


    private void Start()
    {
        _elapsedTime = 0;
    }
    void Update()
    {
        _elapsedTime += Time.deltaTime; 
        if( _elapsedTime > _spawnInterval)
        {
            _spawners[SelectRandomSpawner()].Spawn();
            _elapsedTime = 0;
        }

    }
    private int SelectRandomSpawner()
    {
       
        _nowSpawn = Random.Range(0, _spawners.Length);
        while (_nowSpawn == _lastSpawn)
        {
            _nowSpawn = Random.Range(0, _spawners.Length);
        }
        _lastSpawn = _nowSpawn;
            return _lastSpawn;
    }
}
