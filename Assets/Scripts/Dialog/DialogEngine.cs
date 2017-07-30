using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEngine : MonoBehaviour {

    // Node Buffers. 
    public DialogNode StartNode;
    private DialogNode currentNode;
    private GameObject player;

    void Start()
    {}

    public void StartConversation(GameObject thePlayer)
    {
        currentNode = StartNode;
        player = thePlayer;
        player.GetComponent<Movement>().stopMovement();
        printMessage();
    }

    public void moveToNode(int nodeSelection)
    {
        if (nodeSelection <= (currentNode.getChildNodeCount() - 1) && nodeSelection > -1)
        {
            currentNode = currentNode.NodeList[nodeSelection].GetComponent<DialogNode>();
            printMessage();
            if (currentNode.containsMission())
            {
                // deliver the quest to the user. 
                player.GetComponent<QuestSystem>().assignMission(currentNode.missionToAssign.GetComponent<Mission>());
            }

            if(currentNode.getChildNodeCount() == 0)
            {
                endConversation();
            }
        }
        else
        {
            // TODO - just ignore the input here.
            Debug.Log("ERROR - Message not set");
        }
    }

    public void printMessage()
    {
        Debug.Log(currentNode.getNodeMessage());
    }

    public void setStartNode(DialogNode newStartNode)
    {
        StartNode = newStartNode;
    }

    void endConversation()
    {
        player.GetComponent<Movement>().startMovement();
        player.GetComponent<KeyInput>().endConversationState();
        player = null;
    }
}
