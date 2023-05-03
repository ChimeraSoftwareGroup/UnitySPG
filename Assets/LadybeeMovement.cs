using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybeeMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(transform.position.x >= -3)
            {
                this.gameObject.transform.position = new Vector3((transform.position.x - 1), transform.position.y, transform.position.z);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x <= 3)
            {
                this.gameObject.transform.position = new Vector3((transform.position.x + 1), transform.position.y, transform.position.z);

            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.z <= 8)
            {
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.z >= -5)
            {
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

            }

        }

    }
}
