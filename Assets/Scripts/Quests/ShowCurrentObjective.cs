using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentObjective : MonoBehaviour
{
    private Text t;
    private GameObject player;
    private Mission activeMission;

    bool startOfGame;
    bool description;
    
    void Start()
    {
        startOfGame = true;
        t = GetComponent<Text>();
        t.text = " ";
        description = (gameObject.name == "ObjectiveInfo");
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        activeMission = player.GetComponent<QuestSystem>().getActiveMission();

        if(activeMission != null)
        {
            startOfGame = false;
            t.text = description ? 
                activeMission.GetComponent<Mission>().getActiveObjective().objectiveExtendedDescription :
                activeMission.GetComponent<Mission>().getActiveObjective().objectiveDiscription;
        }
        else if(!startOfGame)
        {
            t.text = description ?
                "People with exclamation marks above their heads have quests for you." : "Find a new quest";
        }
        else
        {
            t.text = description ?
                "Move with the ARROW KEYS. Interact with the E key." : "Find a new quest";
        }
    }
}