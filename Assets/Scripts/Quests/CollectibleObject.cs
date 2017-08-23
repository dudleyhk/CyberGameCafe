using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public string questTag;

    void Update()
    {
        transform.Rotate(0, 0, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if the player enters the collider
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            //update the mission state
            player.GetComponent<QuestSystem>().
                updateMissionState(MissionObjectiveTypes.OBJ_ITEMCOLLECT, questTag);
            
            //destroy this
            Destroy(gameObject);
        }
    }
}
