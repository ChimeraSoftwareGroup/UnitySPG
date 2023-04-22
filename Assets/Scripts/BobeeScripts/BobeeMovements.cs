using UnityEngine;

public class BobeeMovements : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    private Joystick joystick;
    private bool _isStarting = false;
    [SerializeField] GameObject _tutoBobee;
    [SerializeField] GameObject _warningSpider;
    private void Start()
    {
     joystick = FindObjectOfType<Joystick>();
        _isStarting = true;
        // Invoke("StartTuto", 3f);

    }

    private void Update()
    {
        if (_isStarting)
        {
            _tutoBobee.SetActive(true);
            _warningSpider.SetActive(true); 
            _isStarting = false;
            Invoke("StopTutoBobee", 2f);
            Invoke("StopWarning", 2f);
        }
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        float horizontalJoystick = joystick.Horizontal;
        float verticalJoystick = joystick.Vertical;


        Vector3 movement = new Vector3(horizontalJoystick, 0f, verticalJoystick);
        transform.position += movement * speed * Time.deltaTime;


        //Vector3 dir = new Vector3(horizontalJoystick, 0f, verticalJoystick).normalized;
        //if (dir.magnitude >= 0.1f)
        //{
        //    controller.Move(dir * speed * Time.deltaTime);
        //}

    }
    public void StopTutoBobee()
    {
        _tutoBobee.SetActive(false);


    }
    public void StopWarning()
    {
        _warningSpider.SetActive(false);
    }


}
