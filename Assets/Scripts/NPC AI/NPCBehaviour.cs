using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the Waiting State
/// </summary>
public class NPCBehaviour : MonoBehaviour
{
    public State currentState = State.Wait;

   // public NPCWaitState      npcWaitState = null;
   // public NPCTravelState    npcTravelState = null;
   // public NPCSocialiseState npcSocialState = null;
   
    public Player player = null;
    public NPCMovement npcMovement = null;
    public AStar aStar = null;

    public bool inState = false;


    public bool travelling      = false;
    public List<Node> currentPath = null;
    public bool randTargetFound = false;
    public int randTargetNodeID = -1;


    public bool findingPath = false;
    public bool movingTowards = false;

    // Call AStart.Search to create the shortest path. 
    // Call the Characters NPCMovement.BeingTravels() to start the movement cycle.  NPCMovement.BeingTravels(path);


    // Stage One:
    // - Remove anything which doesn't add to this stage.
    // - Aim: Get a loop between traveling and waiting. 
    // - Steps:
    //      o Get path to a random point
    //      o Include sanity checks and retrys if a point cannot be travelled to. 
    //      o move towards the point 
    //      o Repeat this. 
    // - Bugs: 
    //      o Coroutines are causing everything to be done loads of times. 
    //




    public enum State
    {
        Wait, 
        Travel,
        Socialise
    }
    

    /// <summary>
    /// When the player logs in. This function will be called to being the NPC behaviours.
    /// </summary>
    public void Awake()
    {
        // TODO: Get the currentNodeID which the npc has been spawned at. 

        //StartCoroutine(NPCStateMachine());   

    }


    public void Update()
    {
        switch (currentState)
        {
            case State.Wait:
                Waiting();
                break;

            case State.Travel:
                Travel();
                break;

            case State.Socialise:
                
                break;
        }
    }


    /// <summary>
    /// Basic waiting functionality. Pick a random target and find the path for it. 
    /// </summary>
    private void Waiting()
    {
        if (Pathfinding())
            currentState = State.Travel;
    }


    private bool Pathfinding()
    {
        if (!findingPath)
        {
            randTargetNodeID = Random.Range(0, GridManager.Instance.TotalNodes);
            print("Random no. is " + randTargetNodeID);


            if (aStar.CanPathBeFound(npcMovement.CurrentNode.ID, randTargetNodeID))
            {
                findingPath = true;
                StartCoroutine(aStar.PathSearchLoop());
            }
        }

        if (aStar.PathFound())
        {
            print("Path found");
            currentPath = aStar.path;
            aStar.ClearLists();
            findingPath = false;
            return true;
        }
        return false;
    }


    /// <summary>
    /// simple travelling
    /// </summary>
    private void Travel()
    {
        if (MoveTowards())
            currentState = State.Socialise;
    }



    private bool MoveTowards()
    {
        if (!movingTowards)
        {
            if (npcMovement.JourneyToTarget(currentPath))
            {
                movingTowards = false;
                return true;
            }
            movingTowards = true;
        }
        return false;
    }





    /// <summary>
    /// Main State Machine Loop. Based on the currentState run the loop
    /// </summary>
    /// <returns></returns>
    //private IEnumerator NPCStateMachine()
    //{
    //    int i = 0;
    //    // This could be while (!player.LoggedIn && 
    //    while(true)
    //    {
    //        print("current state: " + currentState);
    //       switch(currentState)
    //        {
    //            case State.Wait:
    //                Debug.Log("WAIT STATE");
    //                print("inState is " + inState);
    //                if (!inState)
    //                {
    //                    print("instance no: " + i++);
    //                    StartCoroutine(WaitState(flag =>
    //                    {
    //                        if (flag)
    //                        {
    //                            inState = false;
    //                        }
    //                        else
    //                        {
    //                            inState = true;
    //                        }
    //                    }));
    //                }
    //                break;

    //            case State.Travel:

    //                print("TRAVEL STATE");
    //                print("inState is " + inState);
    //                if (!inState)
    //                {
    //                    print("instance no: " + i++);
    //                    StartCoroutine(TravelState(flag =>
    //                    {
    //                        if (flag)
    //                        {
    //                            inState = false;
    //                        }
    //                        else
    //                        {
    //                            inState = true;
    //                        }
    //                    }));
    //                }
    //                break;

    //            default:
    //                print("No State is set. Do something about it. ");
    //                break;
    //        }
    //        yield return null;
    //    }
    //    yield return true;
    //}



    //private IEnumerator WaitState(System.Action<bool> stateComplete)
    //{
    //    while (true)
    //    {
    //        currentState = npcWaitState.Waiting();




    //        if(currentState != State.Wait)
    //        {
    //            break;
    //        }
    //        stateComplete(false);
    //        yield return null;
    //    }
    //    stateComplete(true);
    //    yield return true;
    //}



    //private IEnumerator TravelState(System.Action<bool> stateComplete)
    //{
    //    // run state loop
    //    while (true)
    //    {
    //        currentState = npcTravelState.Travelling();
    //        if(currentState != State.Travel)
    //        {
    //            break;
    //        }
    //        stateComplete(false);
    //        yield return null;
    //    }
    //    stateComplete(true);
    //    yield return true;
    //}
}
