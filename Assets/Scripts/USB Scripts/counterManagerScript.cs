using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterManagerScript : MonoBehaviour 
{

	private int usbCounter;

	// Use this for initialization
	void Start () 
	{
		usbCounter = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown("space"))
		{
			ResetCounter();
		}

		if(Input.GetKeyDown("e") && usbCounter != 0)
		{
			DecrementCounter();
		}
	}

	public void UpdateCounter()
	{
		usbCounter++;
		Debug.Log (usbCounter);
	}

	public void DecrementCounter()
	{
		usbCounter--;
		Debug.Log (usbCounter);
	}

	public void ResetCounter()
	{
		usbCounter = 0;
		Debug.Log (usbCounter);
	}

}
