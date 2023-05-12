using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] GameObject _Gfx;
    public float screenWidth;
    private bool _isTutoStart = false;
    private bool _isPlaying;
    private bool _isPressedLeft;
    private bool _isPressedRight;
    [SerializeField] GameObject _tutoCanvas;

    [SerializeField] ParticleSystem _dust;
    private void Start()
    {
        _tutoCanvas.SetActive(false);
        _isTutoStart = true;
        if (_isTutoStart)
        {
            StartTuto();
        }
        screenWidth = Screen.width;
       
    }
    private void Update()
    {
        _isPlaying = false;

        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > screenWidth / 2)
            {
                GoLeft();

            }
            if (Input.GetTouch(i).position.x < screenWidth / 2)
            {
               
                GoRight();

            }
            i++;
        }
    }

    public void GoLeft()
    {
        _isPlaying = true;
            float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            MovePlayer(1.0f);
         //    DustPlay();
            _isPlaying = false;
    }

    public void GoRight()
    {
       
       
            _isPlaying = true;
            float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            MovePlayer(-1.0f);
       // DustPlay();

        _isPlaying = false;

        
    }

    //public void DustPlay()
    //{
    //    if(_isPlaying)
    //    {
    //        _dust.Play();
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

    private void FixedUpdate()
    {
       
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
        Flip(rb.velocity.x);
        // float characterVelocity = Mathf.Abs(rb.velocity.x);
        // animator.SetFloat("Speed", rb.velocity.x);

    }

    public void StopTuto()
    {
        _tutoCanvas.SetActive(false);
        _isTutoStart = false;

    }
    public void StartTuto()
    {
        _isTutoStart = false;
        print("Tuto Canvas");
        _tutoCanvas.SetActive(true);
        Invoke("StopTuto", 2f);

    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector3(_horizontalMovement, rb.velocity.y, 0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        rb.AddForce(new Vector3(_horizontalMovement * moveSpeed * Time.deltaTime, 0));
       

    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f){
            _Gfx.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
        else if(_velocity < -0.1f)
        {
            _Gfx.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

    }

    public void Dust()
    {
        _dust.Play();
    }
}
