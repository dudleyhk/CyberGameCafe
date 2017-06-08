using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {


    private float speed;
    public VirtualJoysticks movementStick;

    void Start()
    {
        speed = 0.05f;
    }
    
	void FixedUpdate ()
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
}
