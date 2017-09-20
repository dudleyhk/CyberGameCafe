using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volTrigger : MonoBehaviour {

    MissionObjectiveTypes missionType = MissionObjectiveTypes.OBJ_EVENT;
    public string missionTag;

    // test of quest system. This should be updated when ever a collision occurs.
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<QuestSystem>().updateMissionState(missionType, missionTag);
        }
    }
}
