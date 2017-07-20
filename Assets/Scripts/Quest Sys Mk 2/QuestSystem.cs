using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private List<Mission> currentMissions;
    private int activeMission;


    // utility functions.
    void setActiveMission(int newActiveMission)
    {
        // sets the new mission from the current missions list.
        activeMission = newActiveMission;
    }

    // hands in the chosen mission by the player. 
    void handInMission(int missionToHandIn)
    {
        // TODO - add more code to check and make sure that the mission is complete. 
        currentMissions.RemoveAt(missionToHandIn);
    }

    Mission getCurrentMission()
    {
        return currentMissions[activeMission];
    }

    void updateMissionState(MissionObjectiveTypes missionType, string missionTag)
    {
         currentMissions[activeMission].updateActiveMissionObjectives(missionType, missionTag);
    }


    public void assignMission(Mission newMission)
    {
        if (currentMissions.Count == 0)
        {
            activeMission = 0;
        }

        currentMissions.Add(newMission);
    }
}
