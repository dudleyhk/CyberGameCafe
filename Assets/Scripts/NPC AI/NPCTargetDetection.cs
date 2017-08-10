using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTargetDetection : MonoBehaviour
{
    public NPCMovement npcMovement;
    public NPCBehaviour npcBehaviour;



    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collision with " + other.name);

        if (npcBehaviour.currentState != NPCBehaviour.State.Travel)
            return;


        if (other.tag == "Node")
        {
            print("Node hit");
            Node otherNode = other.gameObject.GetComponent<Node>();
            if (otherNode && otherNode.ID == npcMovement.nextNodeID)
            {
                print("Other node id is equal to next node id");
                npcMovement.tempCurrentID++;
                npcMovement.CurrentNode = npcMovement.Path[npcMovement.tempCurrentID];
                npcMovement.currentNodeID = npcMovement.CurrentNode.ID;
            }
        }
    }

}
