using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Movement : MonoBehaviour {


    private float speed;
    public GameObject joystick;
    public VirtualJoysticks movementStick;
    //private const string EVENT_TOUCH_AQUIRED = "event_touch_aquired";

    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick");
        movementStick = joystick.GetComponent<VirtualJoysticks>();
        speed = 0.05f;
        //Events.Listen(EVENT_ACHEIVEMENT, TouchIsHappening);
    }
    
	void FixedUpdate ()
    {
        
#if UNITY_ANDROID
        if (movementStick.InputDirection.x * movementStick.InputDirection.x >
            movementStick.InputDirection.z * movementStick.InputDirection.z)
        {
            transform.Translate(movementStick.InputDirection.x * speed, 0, 0);
        }
        else
        {
            transform.Translate(0, movementStick.InputDirection.z * speed,0);
        }

#else
        transform.Translate(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0);
#endif

    }

    public void stopMovement()
    {
        speed = 0.0f;
    }

    public void startMovement()
    {
        speed = 0.05f;
    }

    //private void TouchIsHappening()
    //{
    //    Debug.Log("Event " + EVENT_TOUCH_AQUIRED + " is activated");
    //}
}
