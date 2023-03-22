using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTouch : MonoBehaviour
{
   
    void Update()
    {
        if(Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            }

         
        }
    }
}
