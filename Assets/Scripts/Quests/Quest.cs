using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class - Quest Types
// Discription - A set of flags that is used to identify actions for quests.
public enum QuestTypes
{
    // If you want to create a new quest create an entry for the event here. 
    // All items and actions\minigames will have a quest flag which if the quest has been collected we can update the quest status.
    QUEST_COLLECT_USB = 0,
    QUEST_COLLECT_DVD,
}


public class Quest : MonoBehaviour
{
    public string questName;
    public string questDiscription;
    public List<GameObject> objectives;

    // TODO - change objective requirements to 
    public int ObjectiveRequirements;
    // TODO - Add code to check and update quest status during an event. 
    // Better idea we can have the server carry out the checks when it recieved the quest. 
}
