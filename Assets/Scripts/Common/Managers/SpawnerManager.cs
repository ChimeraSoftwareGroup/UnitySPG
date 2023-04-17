using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] Spawner[] _spawners;
    [SerializeField] float _minSpawnInterval;
    [SerializeField] float _maxSpawnInterval;
    private float _currentSpawnInterval = 3;
    private float _elapsedTime = 0;
    private int _lastSpawn;
    private int _nowSpawn;
    public SpawnerManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _currentSpawnInterval && _spawners.Length != 0) 
             {
            try
            {
                _spawners[SelectRandomSpawner()].Spawn();
            }
            catch
            {
                return;
            }
              
                _elapsedTime = 0;
             }
    }

    public void ActivateSpawners()
    {
        gameObject.SetActive(true);
        _elapsedTime = 0;
        _spawners = new Spawner[0];
        Spawner[] spawnersTemp = GameObject.FindObjectsOfType<Spawner>();
        if (spawnersTemp.Length == 0) return;
        _spawners = spawnersTemp;
    }

    public void DeactivateSpawners()
    {
        _spawners = new Spawner[0];
        gameObject.SetActive(false);
    } 

    private int SelectRandomSpawner()
    {
        
        _nowSpawn = Random.Range(0, _spawners.Length);
        if (_nowSpawn == _lastSpawn)
        {
            SelectRandomSpawner();
        }
        _lastSpawn = _nowSpawn;
        _currentSpawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        return _lastSpawn;
       
    }
}
