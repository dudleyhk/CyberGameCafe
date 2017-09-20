using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectDialogue : MonoBehaviour {

	GameObject player;
	Mission thisQuest;
	bool playerInBox;

	bool[] talkedToYet = new bool[5];

	private Interact speak;

	void Start()
	{
		speak = GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<Interact> ();
	}

	string getOfficialStatement()
	{
		switch (gameObject.name)
		{
		case "Jackie":
			if (!talkedToYet [0]) {
				talkedToYet [0] = true;
				thisQuest.updateActiveMissionObjectives (MissionObjectiveTypes.OBJ_EVENT, "questionSusps");
			}
			return "Marlon is innocent.\n\nTito is lying in his first statement.";
		case "Jermaine":
			if (!talkedToYet [1]) {
				talkedToYet [1] = true;
				thisQuest.updateActiveMissionObjectives (MissionObjectiveTypes.OBJ_EVENT, "questionSusps");
			}
			return "The culprit's first name begins with a 'J'\n\nThe culprit's first name begins with an 'M'";
		case "Marlon":
			if (!talkedToYet [2]) {
				talkedToYet [2] = true;
				thisQuest.updateActiveMissionObjectives (MissionObjectiveTypes.OBJ_EVENT, "questionSusps");
			}
			return "It was Tito.\n\nIf Jackie is innocent then Michael is too.";
		case "Michael":
			if (!talkedToYet [3]) {
				talkedToYet [3] = true;
				thisQuest.updateActiveMissionObjectives (MissionObjectiveTypes.OBJ_EVENT, "questionSusps");
			}
			return "I am innocent.\n\nJermaine's second statement is true.";
		case "Tito":
			if (!talkedToYet [4]) {
				talkedToYet [4] = true;
				thisQuest.updateActiveMissionObjectives (MissionObjectiveTypes.OBJ_EVENT, "questionSusps");
			}
			return "It was either Jackie, Marlon or me.\n\nIt was either me, Jermaine or Michael";
		default:
			return "error";
		}
	}

	void Awake()
	{
		for (int i = 0; i < 5; i++) {
			talkedToYet [i] = false;
		}
		thisQuest = GetComponentInParent<Mission> ();
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
			dialogue ();
		}
	}

	void dialogue()
	{
		GameObject textBox = GameObject.FindGameObjectWithTag ("TextBox");

		DialogueMessages d = textBox.GetComponent<DialogueMessages> ();

		if (thisQuest.getActiveObjective () == null) {
			d.spawnTextBox("I think Janet is unhappy about something.\n\nI hate rectangles.",gameObject.name);
		} else{
			d.spawnTextBox (getOfficialStatement (), gameObject.name);
		}
	}
}
