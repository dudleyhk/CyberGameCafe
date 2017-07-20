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

    void StartConversation(GameObject thePlayer)
    {
        currentNode = StartNode;
        player = thePlayer;
    }

    public void moveToNode(int nodeSelection)
    {
        if(nodeSelection <= (currentNode.getChildNodeCount() - 1))
        {
            currentNode = currentNode.NodeList[nodeSelection].GetComponent<DialogNode>();

            if (currentNode.containsMission())
            {
                // deliver the quest to the user. 
                player.GetComponent<QuestSystem>().assignMission(currentNode.missionToAssign.GetComponent<Mission>());
            }
        }
        else
        {
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
        player = null;
    }
}
