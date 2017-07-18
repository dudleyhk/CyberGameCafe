using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEngine : MonoBehaviour {

    // Node Buffers. 
    public DialogNode StartNode;
    private DialogNode currentNode;

    void Start()
    {}

    void StartConversation()
    {
        currentNode = StartNode;
    }

    public void moveToNode(int nodeSelection)
    {
        if(nodeSelection <= (currentNode.getChildNodeCount() - 1))
        {
            currentNode = currentNode.NodeList[nodeSelection];
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
}
