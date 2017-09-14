using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    // This is a collection of objectives and 
    public string missionName;
    public string missionDiscription;

    public GameObject[] missionObjectives;
    [SerializeField]
    private List<MissionObjective> activeObjectives;

    public bool compleated;

    [SerializeField]
    private int compleatedObjectives = 0;

    public int noOfStartingObjectives;


    // utility functions go here. 
    public MissionObjective getActiveObjective()
    {
        if (activeObjectives.Count > 0)
        {
            return activeObjectives[0];
        }
        else
        {
            return null;
        }
    }

    public void updateActiveMissionObjectives(MissionObjectiveTypes objectiveType, string objectiveTag)
    {
        int compleatedActiveObjectives = 0;
        // update the active missions and update the objectives. (TODO - return to update with behaviour for item systems)
        for(int i = 0; i < activeObjectives.Count; i++)
        {
           if(activeObjectives[i].objectiveType == objectiveType && activeObjectives[i].getObjectiveTag() == objectiveTag)
           {
                activeObjectives[i].updateProgressState();
           }

           if (activeObjectives[i].isComplete())
           {
                compleatedActiveObjectives++;

                if (activeObjectives[i].nextObjectivesToRetrieve == 0)
                {
                    compleated = true;
                    break;
                }

            }
        }

        if(compleatedActiveObjectives == activeObjectives.Count && !compleated)
        {
            compleatedObjectives += compleatedActiveObjectives;
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
        

        for(int i = compleatedObjectives; i < compleatedObjectives + objectivesToRetrieve; i++)
        {
            newObjective = missionObjectives[i].GetComponent<MissionObjective>();

            if(newObjective.getObjectiveType() == MissionObjectiveTypes.OBJ_ITEMCOLLECT)
            {
                // TODO - Add code to retrieve items using tag.
                // TODO - if objective is complete it should be tagged as sand added for the system to handle. 
            }

            activeObjectives.Add(newObjective);
            //Debug.Log(newObjective.name + " added to the list");
        }

        if(objectivesToRetrieve == 0)
        {
            compleated = true;
            //Debug.Log("Quest Completed");
        }
    }

    public void resetMission()
    {
        compleatedObjectives = 0;
        for (int i = 0; i < noOfStartingObjectives; i++)
        {
            activeObjectives.Add(missionObjectives[i].GetComponent<MissionObjective>());
        }
    }


    public void startMission()
    {
        compleatedObjectives = 0; 
        activeObjectives = new List<MissionObjective>();
        for (int i = 0; i < noOfStartingObjectives; i++)
        {
            activeObjectives.Add(missionObjectives[i].GetComponent<MissionObjective>());
        }
    }
}
