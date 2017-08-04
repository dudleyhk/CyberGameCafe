using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volTrigger : MonoBehaviour {

    // test of quest system. This should be updated when ever a collision occurs.
	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<QuestSystem>().updateMissionState(GetComponent<MissionEvent>().eventType, GetComponent<MissionEvent>().eventTag);
        }
    }
}
