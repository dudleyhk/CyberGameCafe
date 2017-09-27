using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkDialog : MonoBehaviour
{

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
            GameObject.FindGameObjectWithTag("TextBox").GetComponent<DialogueMessages>();
        if (thisQuest.getActiveObjective() == null)
        {
            d.spawnTextBox("There were just some people in this room using the computers.", name);
            d.spawnTextBox("Every single one of them left the PCs logged into their bank accounts!", name);
            d.spawnTextBox("If the wrong sort got their hands on the computers now they could steal a lot of money from innocent people.", name);
            d.spawnTextBox("I don't really understand technology, so I couldn't find my way around the maze on each computer to find the log out button.", name);
            d.spawnTextBox("You seem very smart, could you please try?", name);
            d.spawnTextBox("Talk to me again once you're ready to go in!", name);
            player.GetComponent<QuestSystem>().assignMission(thisQuest, gameObject);
        }
        else if (!d.gameObject.GetComponent<Image>().IsActive())
        {
            Application.LoadLevel("PublicPCQuest");
        }
    }
}
