using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentObjective : MonoBehaviour
{
    private Text t;
    private GameObject player;
    private Mission activeMission;
    
    void Start()
    {
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

        if(activeMission != null)
        {
            t.text = activeMission.GetComponent<Mission>().getActiveObjective().objectiveDiscription;
        }
        else
        {
            t.text = "Find a new quest";
        }
    }
}