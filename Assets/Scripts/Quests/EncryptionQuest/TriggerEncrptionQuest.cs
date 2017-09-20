using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEncrptionQuest : MonoBehaviour
{
    private GameObject player;
    private bool playerInBox;


	private Interact speak;

	void Start()
	{
		speak = GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<Interact>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInBox == true && speak.beingPressed)
        {
			player.GetComponent<QuestSystem> ().updateMissionState (MissionObjectiveTypes.OBJ_EVENT, "encrypt");
			GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<LoadGame>().writeToTextFile();
            Application.LoadLevel("EncryptionGame");
        }
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
}
