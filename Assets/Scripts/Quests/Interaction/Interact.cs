using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	bool activated;
	public bool beingPressed;

	void Start()
	{
		activated = true;
	}

	void Update () 
	{
		if (activated && (Input.GetButtonDown("Interact")))
        {
			beingPressed = true;
		}
		else
		{
			beingPressed = false;
		}
	}

	public void toggleInteraction()
	{
		activated = !activated;
	}
}
