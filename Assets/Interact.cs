using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	bool enabled;
	public bool beingPressed;

	void Start()
	{
		enabled = true;
	}

	void Update () 
	{
		if (enabled && Input.GetKeyDown (KeyCode.E)) {
			beingPressed = true;
		}
		else
		{
			beingPressed = false;
		}
	}

	public void toggleInteraction()
	{
		enabled = !enabled;
	}
}
