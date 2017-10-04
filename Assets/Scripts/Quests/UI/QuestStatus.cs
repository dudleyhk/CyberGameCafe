using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatus : MonoBehaviour {

    private GameObject questStatusUI;

    // Comprises a row in the panel. Displays the compleated quests in a list
    public GameObject questName;
    public GameObject questCompleteIcon;

	// Use this for initialization
	void Awake()
    {
        questStatusUI = GameObject.FindGameObjectWithTag("QuestPanel");		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void createQuestDisplay()
    {

    }
}
