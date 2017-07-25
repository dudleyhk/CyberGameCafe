using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testingScript : MonoBehaviour {

    // Start a conversation to test the quest system.
    void OnTriggerEnter(Collider col)
    {
        GameObject playerObject = col.gameObject;

        if(playerObject.tag == "Player")
        {
            playerObject.GetComponent<Movement>().stopMovement();

            // TODO - add code to start conversation with the NPC to test the dialog and quest system. 
        }
    }
}
