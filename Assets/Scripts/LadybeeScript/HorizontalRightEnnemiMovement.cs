using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRightEnnemiMovement : ObjectsToSpawn
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
            this.gameObject.transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
            currentTime = 0.0f;
        }
      

    }
}
