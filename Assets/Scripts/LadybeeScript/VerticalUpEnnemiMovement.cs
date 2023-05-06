using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalUpEnnemiMovement : ObjectsToSpawn
{
    private float currentTime = 0.0f;
    private float duration = 1.0f;

    void Start()
    {

    }

    void Update()
    {
        currentTime += Time.deltaTime;
        float t = Mathf.Clamp01(currentTime / duration);
        if (t >= 0.5f)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x , transform.position.y , transform.position.z + 2);
            currentTime = 0.0f;
        }


    }
}
