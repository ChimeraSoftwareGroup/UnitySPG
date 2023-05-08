using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeNeckMesh : ObjectsToSpawn
{
    private void Start()
    {
    //   
    }
    public override void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Scroll")
        {
            DestroyNeckMesh();
            print("Collision");
        }
    }

    void DestroyNeckMesh()
    {
        Destroy(gameObject);
    }
}
