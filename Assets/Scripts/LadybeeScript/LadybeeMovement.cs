using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybeeMovement : MonoBehaviour
{
    [SerializeField] GameObject _tutoPanel;
    [SerializeField] ParticleSystem _dust;
    [SerializeField] GameObject _Gfx;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        Invoke("StartTutoAnimation", 3f);
        spriteRenderer = _Gfx.GetComponent<SpriteRenderer>();

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
            if (transform.position.z >= -3)
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

        Flip();

        _dust.Play();

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

        Flip();

        _dust.Play();

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
        Flip();

        _dust.Play();

    }
    public void Down()
    {
        if (transform.position.z >= -3)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            if (_direction != 4)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                _direction = 4;
            }
        }

        Flip();
        _dust.Play();

    }

    private void Flip()
    {
        if (!spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;

        }
        else if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void StartTutoAnimation()
    {
        _tutoPanel.SetActive(true);

    }
}
