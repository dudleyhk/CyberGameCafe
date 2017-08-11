using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the Waiting State
/// </summary>
public class NPCBehaviour : MonoBehaviour
{
    public State currentState = State.Wait;

    public NPCMovement npcMovement = null;
    public int randTargetNodeID = -1;


    public enum State
    {
        Wait,
        Travel,
        Socialise
    }


    private void ResetVariables()
    {
        randTargetNodeID = -1;
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentState = State.Wait;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentState = State.Travel;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentState = State.Socialise;
        }

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
                Socialise();
                break;

            default:
                Debug.Log("No State selected");
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
        //if (randTargetNodeID == -1)
        //{
        //   // randTargetNodeID = Random.Range(0, GridManager.Instance.TotalNodes);
        //    print("Random no. is " + randTargetNodeID);
        //}


        //if (!aStar.ValidateTarget(randTargetNodeID) || (randTargetNodeID == npcMovement.CurrentNode.ID))
        //{
        //    Debug.Log("Validation of random target node ID Value failed");
        //    randTargetNodeID = -1;
        //}
        //else
        //{
        //    aStar.StartPathFinding(npcMovement.CurrentNode.ID);
        //    if (aStar.pathAquired)
        //    {
        //        npcMovement.Path = new List<Node>(aStar.Path);
        //        aStar.ResetVariables();
        //        Debug.Log("Path with count " + npcMovement.Path.Count + " Found");
        //        return true;
        //    }
        //}
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
        //npcMovement.Move();
        //if(npcMovement.pathComplete)
        //{
        //    npcMovement.ResetVariables();
        //    return true;
        //}
        return false;
    }


    private void Socialise()
    {
        ResetVariables();
    }
}