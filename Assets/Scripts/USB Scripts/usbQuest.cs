using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usbQuest : MonoBehaviour 
{
	[SerializeField]
	private Mission usbMission;

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
            DialogueMessages d = controller.GetComponent<DialogueMessages>();
            if (usbMission.getActiveObjective() == null)
            {
                d.spawnTextBox("Hi, I am Dov.\nDid you know that you should never insert a suspicious USB stick into a PC?", gameObject.name);
                d.spawnTextBox("Someone obviously doesn't, because they keep trying to insert USB sticks that they found lying around into my computer.", gameObject.name);
                d.spawnTextBox("Please can you help me by moving my computer out of the way so they can't get their sticks in?", gameObject.name);
                d.spawnTextBox("Talk to me again when you're ready.", gameObject.name);
                player.GetComponent<QuestSystem>().assignMission(usbMission, gameObject);
            }
            else if(usbMission.isCompleated())
            {
                GameObject scoreController = GameObject.Find("EternalObject");
                if (scoreController)
                {
                    float score = scoreController.GetComponent<EternalScript>().USBScore;
                    int convertedScore = scoreController.GetComponent<ConvertScore>().getRealScore(score, 30, 90);
                    
                    d.spawnTextBox("Good work", gameObject.name);
                    d.spawnTextBox("You did it, you survived for " + score + " seconds. So your score is " + convertedScore + ".", gameObject.name);
                }
                else
                {
                    d.spawnTextBox("Good work.", gameObject.name);
                }
            }
            else
            {
                player.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "dovTalk");
                Application.LoadLevel("USBQuest");
            }
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
