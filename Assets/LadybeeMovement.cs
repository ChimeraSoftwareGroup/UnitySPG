using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybeeMovement : MonoBehaviour
{
    void Start()
    {
        
    }
    private int _direction = 2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(transform.position.x >= -3)
            {
                this.gameObject.transform.position = new Vector3((transform.position.x - 1), transform.position.y, transform.position.z);
                if(_direction != 1)
                {
                   transform.rotation =  Quaternion.Euler(0f, -90f, 0f);
                    _direction = 1;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x <= 3)
            {
                this.gameObject.transform.position = new Vector3((transform.position.x + 1), transform.position.y, transform.position.z);

                if (_direction != 2)
                {

                   transform.rotation =  Quaternion.Euler(0f, 90f, 0f);
                    _direction = 2;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.z <= 8)
            {
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                if (_direction != 3)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    _direction = 3;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.z >= -5)
            {
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                if (_direction != 4)
                {
                   transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    _direction = 4;
                }
            }

        }

    }
}
