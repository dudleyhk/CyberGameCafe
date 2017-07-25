//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using State = NPCBehaviour.State;


//public class NPCWaitState : MonoBehaviour
//{



//    /// <summary>
//    /// Basic waiting functionality. Pick a random target and find the path for it. 
//    /// </summary>
//    private void Waiting()
//    {
//        if (Pathfinding())
//        {
//            currentState = State.Travel;
//        }
//    }


//    private bool Pathfinding()
//    {
//        print("Finding Path: " + findingPath);
//        if (!findingPath)
//        {
//            randTargetNodeID = Random.Range(0, GridManager.Instance.TotalNodes);
//            print("Random no. is " + randTargetNodeID);


//            if (aStar.CanPathBeFound(npcMovement.CurrentNode.ID, randTargetNodeID))
//            {
//                findingPath = true;
//                StartCoroutine(aStar.PathSearchLoop());
//            }
//        }

//        if (aStar.PathFound())
//        {
//            StopCoroutine(aStar.PathSearchLoop());
//            print("Path found");
//            currentPath = new List<Node>(aStar.path);
//            aStar.ClearLists();
//            findingPath = false;
//            return true;
//        }
//        return false;
//    }

//}
