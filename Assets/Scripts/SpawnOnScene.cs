using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnScene : MonoBehaviour
{
    [SerializeField] public int intervalSpawnInTheGame;

    public static SpawnOnScene instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il existe déjà une instance de SpawnOnScene dans la scène");
            return;
        }
        instance = this;
    }
}
