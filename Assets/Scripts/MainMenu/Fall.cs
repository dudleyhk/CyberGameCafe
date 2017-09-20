using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    float rotationSpeed;
    float fallSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(-5, 5);
        fallSpeed = Random.Range(1, 3) / 10;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position -= new Vector3(0, 0.1f, 0);
        transform.Rotate(0, 0, rotationSpeed);
	}
}
