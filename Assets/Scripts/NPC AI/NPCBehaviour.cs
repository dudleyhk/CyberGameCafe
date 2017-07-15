using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the Waiting State
/// </summary>
public class NPCBehaviour : MonoBehaviour
{
    public ushort startNode = 0;
    public ushort destinationNode = 263;

    public State currentState = State.None;
    public NPCMovement npcMovement = null;

    // Call AStart.Search to create the shortest path. 
    // Call the Characters NPCMovement.BeingTravels() to start the movement cycle.  NPCMovement.BeingTravels(path);


    public enum State
    {
        Wait, 
        Travel,
        Socialise,
        None
    }




    private void Awake()
    {
        
    }

    /// <summary>
    /// When the player logs in. This function will be called to being the NPC behaviours.
    /// </summary>
    public void NPCMode()
    {
        StartCoroutine(NPCStateMachine());   
    }




    /// <summary>
    /// Main State Machine Loop. Based on the currentState run the loop
    /// </summary>
    /// <returns></returns>
    private IEnumerator NPCStateMachine()
    {
        // This could be while (!player.LoggedIn && 
        while(true)
        {
           switch(currentState)
            {
                case State.Wait:
                    StartCoroutine(WaitState());
                    break;

                case State.Travel:
                    StartCoroutine(TravelState());
                    break;

                case State.Socialise:
                    StartCoroutine(SocialiseState());
                    break;

                default:
                    print("No State is set. Do something about it. ");
                    break;
            }
            yield return null;
        }
        yield return true;
    }



    private IEnumerator WaitState()
    {
        // run state loop
        while (true)
        {
            //	Check for players near-by.
            //	Locate target.
                //	How is this done.
                    //	Maybe different areas are checked first, or sections of the map are checked one by one.   	
            //	If new target is acquired change state to Travel.




            yield return null;
        }
        yield return true;
    }



    private IEnumerator TravelState()
    {
        // run state loop
        while (true)
        {
            npcMovement.BeingTravels(AStar.Instance.path);
            // Move towards target node.
            // If the next node they will stand on is occupied.Set State to Wait.
            // Analyse the characters they walk by to determine if they should stop. 




            yield return null;
        }
        yield return true;
    }


    private IEnumerator SocialiseState()
    {
        // run state loop
        while (true)
        {
            //	Pick from events to talk about. 
            //	Do some other sort of socialising checks?
            //	Determine if they should leave the socialising state.




            yield return null;
        }
        yield return true;
    }


}
