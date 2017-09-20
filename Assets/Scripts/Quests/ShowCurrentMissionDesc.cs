using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentMissionDesc : MonoBehaviour
{
	private Text t;
	private GameObject player;
	private Mission activeMission;

	private bool startOfGame;

	void Start()
	{
		startOfGame = true;
		t = GetComponent<Text>();
		t.text = " ";
	}

	void Update()
	{
		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		activeMission = player.GetComponent<QuestSystem>().getActiveMission();

		if (activeMission != null) {
			startOfGame = false;
			t.text = activeMission.GetComponent<Mission> ().getActiveObjective ().objectiveExtendedDescription;
		} else if (startOfGame) {
			t.text = "Use the ARROW KEYS to move your player.\nTalk to people with the 'E' key.";
		}
		else 
		{
			t.text = "Find someone else with a quest for you.";
		}
	}
}