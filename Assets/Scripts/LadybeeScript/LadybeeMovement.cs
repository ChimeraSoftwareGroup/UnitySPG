using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybeeMovement : MonoBehaviour
{
    [SerializeField] GameObject _tutoPanel;

    void Start()
    {
        Invoke("StartTutoAnimation", 0.2f);

    }
    private int _direction = 2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(transform.position.x >= -3)
            {
                this.gameObject.transform.position = new Vector3((transform.position.x - 2), transform.position.y, transform.position.z);
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
                this.gameObject.transform.position = new Vector3((transform.position.x + 2), transform.position.y, transform.position.z);

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
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
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
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
                if (_direction != 4)
                {
                   transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    _direction = 4;
                }
            }

        }

    }

    public void Up()
    {
        if (transform.position.z <= 8)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            if (_direction != 3)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _direction = 3;
            }
        }
    }
    public void Left()
    {
        if (transform.position.x >= -3)
        {
            this.gameObject.transform.position = new Vector3((transform.position.x - 2), transform.position.y, transform.position.z);
            if (_direction != 1)
            {
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                _direction = 1;
            }
        }
    }
    public void Right()
    {
        if (transform.position.x <= 3)
        {
            this.gameObject.transform.position = new Vector3((transform.position.x + 2), transform.position.y, transform.position.z);

            if (_direction != 2)
            {

                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                _direction = 2;
            }
        }
    }
    public void Down()
    {
        if (transform.position.z >= -5)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            if (_direction != 4)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                _direction = 4;
            }
        }
    }
    private void StartTutoAnimation()
    {
        _tutoPanel.SetActive(true);

    }
}
