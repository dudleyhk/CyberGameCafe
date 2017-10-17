using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Movement : MonoBehaviour {
    
	private float defaultSpeed;
    private float speed;
    public GameObject joystick;
    public VirtualJoysticks movementStick;
    //private const string EVENT_TOUCH_AQUIRED = "event_touch_aquired";

    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick");
        if(joystick != null)
            movementStick = joystick.GetComponent<VirtualJoysticks>();
        defaultSpeed = 0.08f;
		speed = defaultSpeed;
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
            transform.Translate(0, movementStick.InputDirection.z * speed, 0);
        }

#else
        float h = Input.GetAxis("Horizontal") + Input.GetAxis("StickHorizontal");
        float v = Input.GetAxis("Vertical") + Input.GetAxis("StickVertical");
        h = (h > 0) ? 1f : (h < 0) ? -1 : 0;
        v = (v > 0) ? 1f : (v < 0) ? -1 : 0;

        transform.Translate(h * speed, v * speed, 0);
#endif

    }

    public void stopMovement()
    {
        speed = 0.0f;
    }

    public void startMovement()
    {
        speed = defaultSpeed;
    }

    public void newPhishingQuestSpeed()
    {
        speed = 0.12f;
    }

    //private void TouchIsHappening()
    //{
    //    Debug.Log("Event " + EVENT_TOUCH_AQUIRED + " is activated");
    //}
}
