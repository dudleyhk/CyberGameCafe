using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject playerPrefab = null;
    public List<GameObject> players = new List<GameObject>();

    public const int maxPathFinders = 3;
    public static int pathFinders = 0;
    public static bool pathFindingLocked = false;

    


    private void LateUpdate()
    {
        //foreach (var player in players)
        //{
        //    var list = SetupMap.nodeGraph.nodes;
        //    var npcMove = player.GetComponentInChildren<NPCMovement>();

        //  v
        //  v

        //    var globalPrevious = Search.GetGlobalNode(npcMove.previousNode, list);
        //    var globalCurrent = Search.GetGlobalNode(npcMove.currentNode, list);
        //    var globalNext = Search.GetGlobalNode(npcMove.nextNode, list);

        //    print("Player: " + player.name);

        //    // Set previous node occupied off (false)
        //    if (globalPrevious != null)
        //    {
        //        print("previous ( " + globalPrevious.label +  ") occupied = false");
        //        globalPrevious.occupied = false;
        //    }

        //    // If the player is between two nodes. Set them to off (false)
        //    if (globalCurrent != null || globalNext != null)
        //    {
        //        if (globalCurrent == globalNext)
        //        {
        //            print("global current = globalNext");
        //            print("global curent and global next set to false");

        //            globalCurrent.occupied = false;
        //            globalNext.occupied = false;
        //            continue;
        //        }
        //    }

        //    // If on the current node set to on. 
        //    if (globalCurrent != null)
        //    {
        //        print("current( " + globalCurrent.label + ") occupied = false");
        //        globalCurrent.occupied = true;
        //    }

        //    // check if another player is on the next node.
        //    if (globalNext != null)
        //    {
        //        print("next ( " + globalNext.label + ") occupied: " + globalNext.occupied);
        //        if (globalNext.occupied)
        //        {
        //            print("paused");
        //            npcMove.pause = true;
        //        }
        //        else
        //        {
        //            print("unpaused");
        //            npcMove.pause = false;
        //        }
        //    }
        //}
    }


    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.A))
        {
            //print("Create New Player");
            SpawnNewPlayer();
        }


        if (pathFinders >= maxPathFinders)
        {
            pathFindingLocked = true;
        }
        else
        {
            pathFindingLocked = false;
        }


        if (pathFinders < 0)
        {
            print("ERROR");
        }
    }


    /// <summary>
    /// Create a new player into the scene at a specified spawn node. 
    /// </summary>
    public void SpawnNewPlayer()
    {
       GameObject player = Instantiate(playerPrefab, this.transform);

        var id = -1;
        do
        {
            id = Random.Range(0, SetupMap.nodeGraph.nodes.Length - 1);
        } while (SetupMap.nodeGraph.nodes[id].solid == true);

        var node = SetupMap.nodeGraph.nodes[id];
        player.GetComponentInChildren<NPCMovement>().currentNode = node;
        player.transform.position = node.position;
        players.Add(player);

        player.name = "Player Character ( ID: " + players.Count + ", Name: Muffasa ) ";
    }

}
