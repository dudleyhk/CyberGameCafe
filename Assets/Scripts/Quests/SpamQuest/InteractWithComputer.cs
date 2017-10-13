using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithComputer : MonoBehaviour {

    private GameObject player;
    private bool playerInBox;
    Mission thisQuest;

    int lastEMail;

	private Interact speak;

	void Start()
	{
		speak = GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<Interact>();
        lastEMail = 9;
        thisQuest = transform.parent.gameObject.GetComponent<Mission>();
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
        if (speak.beingPressed && playerInBox
            && (thisQuest.getActiveObjective().objectiveTag == "checkCom"
            || thisQuest.getActiveObjective().objectiveTag == "findSpam")
            && !GameObject.Find("E-Mail"))
        {
            interact();
        }
    }

    void interact()
    {
        if (thisQuest.getActiveObjective().getObjectiveTag() == "checkCom")
        {
            thisQuest.updateActiveMissionObjectives(MissionObjectiveTypes.OBJ_EVENT, "checkCom");
        }
        int x;
        do {
            x = Random.Range(0, 5);
        } while (x == lastEMail);
        GetComponentInParent<Questions>().printEmail(x);
        lastEMail = x;

        thisQuest.updateActiveMissionObjectives(MissionObjectiveTypes.OBJ_EVENT, "findSpam");
    }
}
