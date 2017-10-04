using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncryptyPlayerMovement : MonoBehaviour {

    [SerializeField]
    float defaultSpeed;
    float speed;

    void Start()
    {
        speed = defaultSpeed;
    }

	void Update ()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0) * speed;

        transform.Translate(movement);
	}

    void OnCollisionStay2D(Collision2D col)
    {
        Vector3 knockback;
        switch(col.gameObject.name)
        {
            case "LeftWall":
                knockback = new Vector3(speed, 0, 0);
                break;
            case "RightWall":
                knockback = new Vector3(-speed, 0, 0);
                break;
            case "TopWall":
                knockback = new Vector3(0,-speed, 0);
                break;
            case "BottomWall":
                knockback = new Vector3(0, speed, 0);
                break;
            default:
                knockback = new Vector3(0, 0, 0);
                break;
        }
        
        transform.Translate(knockback);
    }
}
