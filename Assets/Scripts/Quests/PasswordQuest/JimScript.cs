using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimScript : MonoBehaviour {
    public Mission thisQuest;
    private GameObject player;
    private bool playerInBox;
    private bool interacting;

    GameObject textBox;

    void Start()
    {
        textBox = GameObject.FindGameObjectWithTag("TextBox");
    }

    void Awake()
    {
        playerInBox = false;
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
		if (GameObject.FindGameObjectWithTag("GameController").GetComponent<UseKey>().getUseKey() && playerInBox)
        {
            dialogue();
        }
    }

    void dialogue()
	{
		
        GameObject textBox = GameObject.FindGameObjectWithTag("TextBox");
        DialogueMessages d = textBox.GetComponent<DialogueMessages>();
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<UseKey> ().toggleUseKey (false);

        if (player.GetComponent<QuestSystem>().getActiveMission() != null)
		{
            // consider implimenting a mission search.
            if(player.GetComponent<QuestSystem>().getActiveMission().missionName != "Setting Passwords")
            {
//                d.spawnTextBox("Hello there, Just to let you know we have some new computers here today.", "Jim");
//                d.spawnTextBox("We need some new passwords for these systems,\n so could you sort out some new passwords for these computers thanks.", "Jim");
//
//                player.GetComponent<QuestSystem>().assignMission(thisQuest,gameObject);
//                thisQuest.startMission();
//				GameObject.FindGameObjectWithTag ("GameController").GetComponent<UseKey> ().toggleUseKey (true);
            }
        }
        else
        {
            // Put any additional dialog logic here. 
            d.spawnTextBox("Hello there, Just to let you know we have some new computers here today.", "Jim");
            d.spawnTextBox("We need some new passwords for these systems,\n so could you sort out some new passwords for these computers thanks.", "Jim");

            player.GetComponent<QuestSystem>().assignMission(thisQuest, gameObject);
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<UseKey> ().toggleUseKey (true);
            thisQuest.startMission();
        }
    }
}
