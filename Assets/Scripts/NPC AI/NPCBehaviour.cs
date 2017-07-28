using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the Waiting State
/// </summary>
public class NPCBehaviour : MonoBehaviour
{
    public State currentState = State.Wait;

    public Player player = null;
    public NPCMovement npcMovement = null;
    public AStar aStar = null;

    public List<Node> currentPath = null;
    public int randTargetNodeID = -1;


    public bool findingPath = false;
    public bool movingTowards = false;


    public GameObject waiting = null;




    public enum State
    {
        Wait,
        Travel,
        Socialise
    }

    

    public void Update()
    {
        switch (currentState)
        {
            case State.Wait:
                Debug.Log("WAITING");
                Waiting();
                break;

            case State.Travel:
                Debug.Log("TRAVELING");
                Travel();
                break;

            case State.Socialise:

                break;

            default:
                Debug.Log("No sTate selected");
                break;
        }
    }


    /// <summary>
    /// Basic waiting functionality. Pick a random target and find the _path for it. 
    /// </summary>
    private void Waiting()
    {
        if (PathFinding())
        {
            currentState = State.Travel;
        }
    }



    private bool PathFinding()
    {
        if (randTargetNodeID == -1)
        {
            randTargetNodeID = Random.Range(0, GridManager.Instance.TotalNodes);
            print("Random no. is " + randTargetNodeID);
        }


        if (!aStar.ValidateTarget(randTargetNodeID) || (randTargetNodeID == npcMovement.CurrentNode.ID))
        {
            Debug.Log("Validation of random target node ID Value failed");
            randTargetNodeID = -1;
        }
        else
        {
            aStar.StartPathFinding(npcMovement.CurrentNode.ID);
            if (aStar.pathAquired)
            {
                currentPath = new List<Node>(aStar.Path);
                aStar.ResetVariables();
                Debug.Log("Path with count " + currentPath.Count + " Found");
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// simple travelling
    /// </summary>
    private void Travel()
    {
        if (MoveTowards())
        {
            currentState = State.Socialise;
        }
    }



    private bool MoveTowards()
    {
        Debug.Log("Moving towards");
        if (!movingTowards)
        {
            StartCoroutine(npcMovement.completePath(currentPath));
            movingTowards = true;
        }

        if (npcMovement.JourneyComplete())
        {
            movingTowards = false;
            currentPath = null;
            return true;
        }
        return false;
    }
}