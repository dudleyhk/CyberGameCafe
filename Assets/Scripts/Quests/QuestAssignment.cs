using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAssignment : MonoBehaviour {

    // The Quests that NPCs can assign to players. 
    public Quest[] questsToAssign;
    private List<Quest> activeQuests;

    // TODO - check active quests when a conversation starts. 


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    

    public void handInQuest(Quest questToHandIn)
    {
        for(int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i] == questToHandIn)
            {
                // Add flag to check and see if the quest is complete. 
                // If complete a custom dialog should occur.
            }
            else
            {

            }
        }



    }
}
