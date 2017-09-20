using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterManagerScript : MonoBehaviour 
{
	private int usbCounter;
	private GameObject player;
	// Use this for initialization
	void Start () 
	{
		usbCounter = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void UpdateCounter()
	{
		usbCounter++;
		Debug.Log (usbCounter);
		player.GetComponent<QuestSystem> ().updateMissionState (MissionObjectiveTypes.OBJ_EVENT, "usbPickup");
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
