using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class - DialogNode
// Discription - Contains message for dialog system and refrences to other dialog for player selection.
public class DialogNode : MonoBehaviour {

    private string NodeMessage;
    private string NodeName; 
    public DialogNode[] NodeList;

    public int getChildNodeCount()
    {
        return NodeList.Length;
    }

    public string getNodeMessage()
    {
        return NodeMessage;
    }

    public string getNodeName()
    {
        return NodeName;
    }

    // TODO - consider adding functions to read/write data which will be used in the dialog system.
}
