using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


    private float speed;
    public VirtualJoysticks movementStick;
    //private const string EVENT_TOUCH_AQUIRED = "event_touch_aquired";



    private void Start()
    {
        speed = 0.05f;
        //Events.Listen(EVENT_ACHEIVEMENT, TouchIsHappening);
    }
    
	private void Update ()
    {
        if (movementStick.InputDirection.x * movementStick.InputDirection.x >
            movementStick.InputDirection.z * movementStick.InputDirection.z)
        {
            transform.Translate(movementStick.InputDirection.x * speed, 0, 0);
        }
        else
        {
            transform.Translate(0, movementStick.InputDirection.z * speed,0);
        }
        
    }


    //private void TouchIsHappening()
    //{
    //    Debug.Log("Event " + EVENT_TOUCH_AQUIRED + " is activated");
    //}
}
