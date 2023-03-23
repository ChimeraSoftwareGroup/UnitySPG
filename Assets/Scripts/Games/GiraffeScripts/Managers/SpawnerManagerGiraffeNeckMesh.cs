using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerGiraffeNeckMesh : MonoBehaviour
{
    [SerializeField] float _spawnInterval;
    public Spawner _spawner;
    private float _elapsedTime;

    private void Start()
    {

        GameObject player = GameObject.Find("Player");
        _spawner = player.transform.Find("Giraffe")
            .transform.Find("NeckSpawner")
            .GetComponent<Spawner>();
        _elapsedTime = 0;
    }
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _spawnInterval)
        {
            if (_spawner) _spawner.Spawn();
        }
    }
}
