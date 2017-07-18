using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionObjectiveTypes
{
    OBJ_ITEMCOLLECT = 0,
    OBJ_EVENT
}

public class MissionObjective : MonoBehaviour
{
    // Mission objectives
    public string objectiveDiscription;

    public MissionObjectiveTypes objectiveType;
    public string objectiveTag;

    // the starting and finishing values for the objective. 
    public int startState;
    public int finishState;

    private int currentProgress;
    private bool complete = false;

    // change in progress when progress is updated (needs a better name).
    private int deltaProgressValue;

    // retrieves the next set of objectives after the current set of objectives is complete. 
    public int nextObjectivesToRetrieve;

    #region CLASS_FUNCTIONS //objective utility functions. (consider using properties).


    // increases the progress variable and updates quest compleation state. 
    public void updateProgressState(int increaseBy = 1)
    {
        currentProgress += deltaProgressValue * increaseBy;

        if(currentProgress == finishState)
        {
            complete = true;
        }
    }

    public string getObjectiveTag()
    {
        return objectiveTag;
    }

    public MissionObjectiveTypes getObjectiveType()
    {
        return objectiveType;
    }

    public bool isComplete()
    {
        return complete;
    }

    public string getObjectiveDiscription()
    {
        return objectiveDiscription;
    }

    #endregion
}
