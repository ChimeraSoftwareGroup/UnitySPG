using UnityEngine;

public class BobeeMovements : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    private Joystick joystick;
    private bool _isStarting = false;
    [SerializeField] GameObject _tutoBobee;
    [SerializeField] GameObject _warningSpider;

    [SerializeField] ParticleSystem _pollen;
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
        
        float horizontalJoystick = joystick.Horizontal;
        float verticalJoystick = joystick.Vertical;

        Vector3 direction = new Vector3(horizontalJoystick, 0f, verticalJoystick);
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
        transform.position += direction * speed * Time.deltaTime;
        
        if(direction == Vector3.zero)
        {
            _pollen.Stop();
        }
        else
        {
            _pollen.Play();
        }
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
