using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usbQuest : MonoBehaviour 
{
	[SerializeField]
	private Mission usbMission;
	[SerializeField]
	private MissionObjective usbPickupObjective;
	[SerializeField]
	private MissionObjective usbSortObjective;
    [SerializeField]
    GameObject USBHolder;

	private bool playerEnter;
	private GameObject player;

	private GameObject controller;

	private Interact speak;

	void Start()
	{
		speak = GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<Interact>();
		playerEnter = false;
		controller = GameObject.FindGameObjectWithTag ("TextBox");
	}
		
	// Update is called once per frame
	void Update () 
	{		
		if(playerEnter && speak.beingPressed)
		{

			player.GetComponent<QuestSystem> ().assignMission (usbMission, this.gameObject);
			DialogueMessages d = controller.GetComponent<DialogueMessages> ();
			d.spawnTextBox ("Hello! I am Dov, I'm here for your Cyber Security week USB safety awareness seminar."
				+"\nYou're going to be with me to learn about USB safety. Exciting stuff!", "Dov");
			d.spawnTextBox ("I'm going to give you some USBs, and you're going to sort through them for me."
			+"\nIf you think they're safe to use, please plug them into the computer and see what's on it."
			+"\nOtherwise through them into the rubbish bin over there!", "Dov");
            
                Instantiate(USBHolder);
            
		}
			
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (player == null) 
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		if(coll.gameObject == player)
		{
			playerEnter = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject == player)
		{
			playerEnter = false;
		}
	}
}
