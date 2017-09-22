using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    float rotationSpeed;
    float fallSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(-5, 5);
        fallSpeed = Random.Range(1f, 3f) / 10f;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position += new Vector3(fallSpeed, 0, 0);
        transform.Rotate(0, 0, rotationSpeed);
	}
}
