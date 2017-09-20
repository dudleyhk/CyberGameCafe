using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropUSB : MonoBehaviour 
{
	private BoxCollider2D binColl;
	private BoxCollider2D compColl;
	private GameObject player;

	private int usbCount;
	private int binCount;
	private int compCount;

	// Use this for initialization
	void Start () 
	{
		usbCount = 5;
		binCount = 0;
		compCount = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		binColl = GameObject.FindGameObjectWithTag("bin").GetComponent<BoxCollider2D>();
		compColl = GameObject.FindGameObjectWithTag("computer").GetComponent<BoxCollider2D>();
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		//Debug.Log ("On Trigger Stay");
		if (coll == binColl) 
		{
			if (Input.GetKeyDown(KeyCode.E))
			{		
				player.GetComponent<QuestSystem> ().updateMissionState (MissionObjectiveTypes.OBJ_EVENT, "usbSort");
				if (usbCount > 0 && binCount < 5) 
				{
					usbCount--;
					binCount++;
					Debug.Log (binCount);
				}
			}
		}

		if (coll == compColl) 
		{
			if (Input.GetKeyDown(KeyCode.E))
			{		
				player.GetComponent<QuestSystem> ().updateMissionState (MissionObjectiveTypes.OBJ_EVENT, "usbSort");
				if (usbCount > 0 && compCount < 5) 
				{
					usbCount--;
					compCount++;
					Debug.Log (compCount);
				}
			}
		}

	}
}
