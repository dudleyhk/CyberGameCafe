using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testingScript : MonoBehaviour {

    public GameObject questMenu;
    //// Start a conversation to test the quest system.
    //void OnTriggerEnter(Collider col)
    //{
    //    GameObject playerObject = col.gameObject;

    //    if(playerObject.tag == "Player")
    //    {
    //        this.gameObject.GetComponent<DialogEngine>().StartConversation(playerObject);
    //        // TODO - add code to start conversation with the NPC to test the dialog and quest system. 
    //    }
    //}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //questMenu.GetComponent<QuestStatus>().ToggleCompleationState("Quest 1");
        }
    }
}
