using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = NPCBehaviour.State;


public class NPCWaitState : MonoBehaviour
{

    public NPCMovement npcMovement = null;
    public int targetNodeID = 0;

   


    //public State Waiting()
    //{
    //    //	Check for players near-by.
    //    //	Locate target.
    //    if (AStar.Instance.Search(npcMovement.CurrentNodeID, targetNodeID))
    //    {
    //        print("Search complete");
    //        return State.Travel;
    //    }
    //    return State.Wait;
    //}

}
