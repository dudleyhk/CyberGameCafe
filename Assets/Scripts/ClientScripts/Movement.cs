using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {


    private float speed;
    public VirtualJoysticks movementStick;
    //private const string EVENT_TOUCH_AQUIRED = "event_touch_aquired";



    private void Start()
    {
        speed = 0.05f;
        //Events.Listen(EVENT_ACHEIVEMENT, TouchIsHappening);
    }
    
<<<<<<< HEAD:Assets/Scripts/ClientScripts/Movement.cs
	void FixedUpdate ()
=======
	private void Update ()
>>>>>>> dcc88fbde769e5047b3b0032f9756f9345ae4eb3:Assets/Scripts/Movement.cs
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
