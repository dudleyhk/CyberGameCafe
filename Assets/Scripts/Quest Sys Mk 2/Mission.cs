using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    // This is a collection of objectives and 
    public string missionName;
    public string missionDiscription;

    public GameObject[] missionObjectives;
    private List<MissionObjective> activeObjectives;

    private bool compleated;
    private int compleatedObjectives;

    void Awake()
    {
        compleated = false;
    }


    // utility functions go here. 
    public void updateActiveMissionObjectives(MissionObjectiveTypes objectiveType, string objectiveTag)
    {
        int compleatedObjectives = 0;

        // update the active missions and update the objectives. (TODO - return to update with behaviour for item systems)
        for(int i = 0; i < activeObjectives.Count; i++)
        {
           if(activeObjectives[i].objectiveType == objectiveType && activeObjectives[i].getObjectiveTag() == objectiveTag)
           {
                activeObjectives[i].updateProgressState();

                if (activeObjectives[i].isComplete())
                {
                    compleatedObjectives++;
                }

                break;
            }
        }

        if(compleatedObjectives == activeObjectives.Count)
        {
            getNewActiveObjectives();
        }
    }

    void getNewActiveObjectives()
    {
        MissionObjective newObjective;
        // updates the list of active objectives and retrieves some new ones
        // here is where we retrieve items from the inventory if a quest is a collection one. 
        // else the quest is laballed as complete.

        int objectivesToRetrieve = activeObjectives[activeObjectives.Count - 1].nextObjectivesToRetrieve;
        activeObjectives.Clear();

        for(int i = compleatedObjectives; i < i + objectivesToRetrieve + 1; i++)
        {
            newObjective = missionObjectives[i].GetComponent<MissionObjective>();

            if(newObjective.getObjectiveType() == MissionObjectiveTypes.OBJ_ITEMCOLLECT)
            {
                // TODO - Add code to retrieve items using tag.
            }

            activeObjectives.Add(newObjective);
        }

        if(objectivesToRetrieve == 0)
        {
            compleated = true;
        }
    }
}
