using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            d.spawnTextBox("Public PC Quest Dialog!", name);
            d.spawnTextBox("Talk to me again once you're ready to go in!", name);
            player.GetComponent<QuestSystem>().assignMission(thisQuest, gameObject);
        }
        else
        {
            Application.LoadLevel(3);
        }
    }
}
