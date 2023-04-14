using UnityEngine;

public class BobeeMovements : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    private Joystick joystick;
    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }
   
    private void Update()
    {
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


}
