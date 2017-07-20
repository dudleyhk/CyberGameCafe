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
        print("Random no. is " + randTargetFound);

        // This gets locked when a random value is received and unlocked if the ID 
        //   doesn't work. 
        if (!randTargetFound)
        {
            randTargetNodeID = Random.Range(0, GridManager.Instance.TotalNodes);
            randTargetFound = true;


            print("finding random target");
            print("Target of " + randTargetNodeID + " found");
        }

        // If this function returns false there has been an error regarding the targetID
        //  and another needs to be received.
        if(!aStar.Search(player.spawnNodeID, randTargetNodeID))
        {
            randTargetFound = false;
            print("Astar has had trouble with the rand value");
            print("A new one will be aquired");
        }


        // A path has been found
        if(aStar.Path.Count > 0)
        {
            currentPath = aStar.Path;
            randTargetFound = false;

            currentState = State.Travel;
        }
    }



    /// <summary>
    /// simple travelling
    /// </summary>
    private void Travel()
    {
        Debug.Log("TRAVELLING");
        if (npcMovement.JourneyToTarget(currentPath))
        {
            print("Finished journey.. find path again");
            aStar.ResetPath();
            currentPath.Clear();

            currentState = State.Socialise;
        }
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
