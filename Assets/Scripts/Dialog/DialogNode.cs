using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class - DialogNode
// Discription - Contains message for dialog system and refrences to other dialog for player selection.
public class DialogNode : MonoBehaviour {

    public string NodeMessage;
    public string NodeName; 
    public GameObject[] NodeList;
    public GameObject missionToAssign;

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

    public bool containsMission()
    {
        if(missionToAssign != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // TODO - consider adding functions to read/write data which will be used in the dialog system.
}
