using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
 
    [SerializeField] ObjectsToSpawn _object;

   
   public void Spawn()
    {
        Instantiate(_object, transform.position, Quaternion.identity);
    }
}
