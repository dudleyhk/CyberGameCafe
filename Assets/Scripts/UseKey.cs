using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKey : MonoBehaviour {

	private bool useKeyEnabled = true;
	private bool useKeyDown = false;

	public void toggleUseKey(bool isEnabled)
	{
		useKeyEnabled = isEnabled;
	}

	public bool getUseKey()
	{
		return useKeyDown ? true : false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (useKeyEnabled) 
		{
			useKeyDown = (Input.GetButtonDown("Interact"));
		} 
		else 
		{
			useKeyDown = false;
		}
	}
}
