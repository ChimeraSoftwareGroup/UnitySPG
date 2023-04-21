using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float screenWidth;
    private bool _isTutoStart = false;

    [SerializeField] GameObject _tutoCanvas;

    private void Start()
    {
        _tutoCanvas.SetActive(false);
        _isTutoStart = true;
        if (_isTutoStart)
        {
            Invoke("StartTuto", 3.5f);
        }
        screenWidth = Screen.width;
       
    }
    private void Update()
    {

       
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > screenWidth / 2)
            {
                float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
                MovePlayer(1.0f);
            }
            if (Input.GetTouch(i).position.x < screenWidth / 2)
            {
                float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
                MovePlayer(-1.0f);
            }
            i++;
        }

    }

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
        Invoke("StopTuto", 3f);

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
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
