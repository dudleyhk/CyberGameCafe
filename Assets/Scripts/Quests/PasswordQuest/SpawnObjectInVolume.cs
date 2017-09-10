using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - consider changing class name. 
public class SpawnObjectInVolume : MonoBehaviour {

    // public GameObject objectToSpawn;
    // public Vector3 positionToSpawn;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Instantiate(objectToSpawn, positionToSpawn, Quaternion.Euler(Vector3.zero));
            Debug.Log("collision Occurs");
            col.gameObject.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "passwordGetToComputerLab");
        }
    }
}
