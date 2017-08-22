using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField]
    private List<Mission> currentMissions;
    private int activeMission = 0;

    public Mission getActiveMission()
    {
        return currentMissions.Count > 0 ? currentMissions[0] : null;
    }

    // utility functions.
    void setActiveMission(int newActiveMission)
    {
        // sets the new mission from the current missions list.
        if(newActiveMission < currentMissions.Count && newActiveMission > 0)
        {
            activeMission = newActiveMission;
        }
    }

    // hands in the chosen mission by the player. 
    void handInMission(int missionToHandIn)
    {
        // TODO - add more code to check and make sure that the mission is complete. 
        currentMissions.RemoveAt(missionToHandIn);
    }

    public void updateMissionState(MissionObjectiveTypes missionType, string missionTag)
    {
        if(currentMissions.Count != 0)
        {
            currentMissions[activeMission].updateActiveMissionObjectives(missionType, missionTag);

            if (currentMissions[activeMission].compleated)
            {
                handInMission(activeMission);
            }
        }
    }


    public void assignMission(Mission newMission)
    {
        if (currentMissions == null)
        {
            activeMission = 0;
            currentMissions = new List<Mission>();
        }
        newMission.startMission();
        currentMissions.Add(newMission);
    }
}
