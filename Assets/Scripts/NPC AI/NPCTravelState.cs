using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = NPCBehaviour.State;



public class NPCTravelState : MonoBehaviour
{
    public NPCMovement npcMovement = null;


    public State Travelling()
    {
        if(npcMovement.BeingTravels(AStar.Instance.Path))
        {
            return State.Wait;
        }

        // Analyse the characters they walk by to determine if they should stop. this might return State.Socialise. 


        return State.Travel;
    }
}
