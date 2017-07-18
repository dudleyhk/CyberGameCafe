using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuffer : MonoBehaviour {

    private List<Quest> currentQuestList;

   // private Quest[] finishedQuestList; // (Remember - this may not be nessecery)

    void addQuestToList(Quest newQuest)
    {
        if(currentQuestList.Count != 0)
        {
            currentQuestList[currentQuestList.Count] = newQuest;
        }
        else
        {
            currentQuestList[0] = newQuest;
        }

        // TODO - add code to add quest as active and update quest state based on the inventory. 
    }

    void removeQuestFromList(int questToRemove)
    {
        currentQuestList.RemoveAt(questToRemove);
    }

    public void updateQuestStatus(QuestTypes eventFlag)
    {
        foreach(Quest i in currentQuestList)
        {
            
        }
    }
}
