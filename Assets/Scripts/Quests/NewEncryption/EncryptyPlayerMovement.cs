using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncryptyPlayerMovement : MonoBehaviour {

    [SerializeField]
    float speed;

	void Update ()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0) * speed;

        transform.Translate(movement);
	}
}
