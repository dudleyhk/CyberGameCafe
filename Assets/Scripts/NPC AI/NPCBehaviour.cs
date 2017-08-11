using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the Waiting State
/// </summary>
public class NPCBehaviour : MonoBehaviour
{
    public NPCMovement npcMovement = null;
    public State currentState = State.Wait;
    public bool readyToTravel = false;

    /*Path Finding */
    public GameObject nodeSprite_debug = null;
    public bool generatePath_debug = false;
    public bool generateGoal_debug = false;
    public bool calculating = false;
    public bool getPath = false;
    public Search search;

    /* Travel */
    public bool initTravel = false;
    public bool pauseTravel = false;




    public enum State
    {
        Wait,
        Travel,
        Socialise
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
                //Debug.Log("WAITING");
                Waiting();
                break;

            case State.Travel:
                //Debug.Log("TRAVELING");
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
        if (getPath)
        {
            if (!PathFinding())
            {
                getPath = true;
            }
            else
            {
                getPath = false;
                currentState = State.Travel;
            }
        }


        //if(readyToTravel)
        //{
        //    currentState = State.Travel;
       // }
    }


    /// <summary>
    /// Take all the steps to finding a path to a random goal anywhere in the map. 
    /// </summary>
    /// <returns>Return false if the path is not complete</returns>
    private bool PathFinding()
    {
        if (calculating)
            return false;

        // Get a random goal and check its not a wall. 
        var goal = Random.Range(0, SetupMap.grid.Length - 1);
        if (SetupMap.nodeGraph.nodes[goal].type == 1)
        {
            calculating = false;
            return false;
        }
        else
        {
            calculating = true;
        }


        if (generateGoal_debug)
        {
            GameObject goalSprite = Instantiate(nodeSprite_debug, SetupMap.nodeGraph.nodes[goal].position, Quaternion.identity);
            goalSprite.name = "GOAL NODE";
            goalSprite.GetComponent<SpriteRenderer>().color = Color.red;
            goalSprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }


        search = new Search(SetupMap.nodeGraph);
        search.Start(npcMovement.currentNode, SetupMap.nodeGraph.nodes[goal]);

        while (!search.finished)
        {
            search.Step();
        }

        
        Debug.Log("Search done. Path length " + search.path.Count + " iterations " + search.iterations);


        if (generatePath_debug)
        {
            foreach (var node in search.path)
            {
                GameObject path = Instantiate(nodeSprite_debug, node.position, Quaternion.identity);
            }
        }



        calculating = false;
        return true;
    }



    /// <summary>
    /// simple travelling
    /// </summary>
    private void Travel()
    {
        if (!pauseTravel)
        {
            if (!initTravel)
            {
                npcMovement.Init(search.path);
                initTravel = true;
            }
            else
            {
                if (npcMovement.Move())
                {
                    initTravel = false;
                    currentState = State.Wait;
                }
            }
        }
    }



    private bool MoveTowards()
    {
        
        
        return false;
    }


    private void Socialise()
    {
    }
}