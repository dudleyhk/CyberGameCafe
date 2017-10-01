using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonyDialogue : MonoBehaviour {

	GameObject player;
	bool playerInBox;
	Mission thisQuest;
	[SerializeField]
	GameObject computerTrigger;

	private Interact speak;

	void Start()
	{
		speak = GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<Interact>();
		thisQuest = GetComponentInParent<Mission>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (col.gameObject == player)
		{
			playerInBox = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (col.gameObject == player)
		{
			playerInBox = false;
		}
	}

	void Update()
	{
		if (speak.beingPressed && playerInBox)
		{
			interact();
		}
	}

	void interact()
	{
		DialogueMessages d =
			GameObject.FindGameObjectWithTag ("TextBox").GetComponent<DialogueMessages> ();
		if (thisQuest.getActiveObjective () == null) {
			d.spawnTextBox ("I have some very very important E-Mails to send, but I really need to wait here for a parcel.", name);
			d.spawnTextBox ("As I know you so well, please could you go onto the computer in the room here and send my E-Mails?", name);
			d.spawnTextBox ("Unfortunately the secure server is down and the information is very important. So it must be encrypted.", name);
			d.spawnTextBox ("If the E-Mails don't get encrypted, there is a risk that someone could intercept the E-Mails and learn my secrets.", name);
			d.spawnTextBox ("Each letter in the message needs to translated to another letter.", name);
			d.spawnTextBox ("For example, in a 1 Caeser Shift, B needs to be typed instead of A and C needs to be typed instead of B", name);

			computerTrigger.GetComponent<BoxCollider2D> ().enabled = true;

			player.GetComponent<QuestSystem> ().assignMission (thisQuest, gameObject);
		}
        else if(thisQuest.isCompleated())
        {
            if (GameObject.Find("EternalObject"))
            {
                d.spawnTextBox("Good work, it took you " + GameObject.Find("EternalObject").GetComponent<EternalScript>().encryptionScore + " to encrypt the E-Mail.");
            }
            else
            {
                d.spawnTextBox("Good work pal.", name);
            }
        }
		else if(thisQuest.getActiveObjective().objectiveTag == "encrypt")
		{
			d.spawnTextBox ("I really appreciate your help.", name);
		}
	}
}
