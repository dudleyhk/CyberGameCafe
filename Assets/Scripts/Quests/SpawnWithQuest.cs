using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWithQuest : MonoBehaviour {

    public Mission x;

    // Use this for initialization
	void Start ()
    {
        GetComponent<QuestSystem>().assignMission
            (GameObject.Find("tutorialQuest").GetComponent<Mission>());
	}
}
