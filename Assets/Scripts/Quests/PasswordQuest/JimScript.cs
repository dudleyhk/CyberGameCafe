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
        if (Input.GetKeyDown(KeyCode.E) && playerInBox)
        {
            dialogue();
        }
    }

    void dialogue()
    {
        GameObject textBox = GameObject.FindGameObjectWithTag("TextBox");
        DialogueMessages d = textBox.GetComponent<DialogueMessages>();

        if (player.GetComponent<QuestSystem>().getActiveMission() != null)
        {
            // consider implimenting a mission search.
            if(player.GetComponent<QuestSystem>().getActiveMission().missionName != "Setting Passwords")
            {
                d.spawnTextBox("Hello there, Just to let you know we have some new computers here today.", "Jim");
                d.spawnTextBox("We need some new passwords for these systems,\n so could you sort out some new passwords for these computers thanks.", "Jim");

                player.GetComponent<QuestSystem>().assignMission(thisQuest);
                thisQuest.startMission();
            }
        }
        else
        {
            // Put any additional dialog logic here. 
            d.spawnTextBox("Hello there, Just to let you know we have some new computers here today.", "Jim");
            d.spawnTextBox("We need some new passwords for these systems,\n so could you sort out some new passwords for these computers thanks.", "Jim");

            player.GetComponent<QuestSystem>().assignMission(thisQuest);
            thisQuest.startMission();
        }
    }
}
