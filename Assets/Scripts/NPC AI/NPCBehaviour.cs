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
    public bool calculating = false;
    public bool getPath = false;
    public int goal = -1;
    public Search search;
    public bool generatePath_debug = false;

    /* Travel */
    public bool initTravel = false;
    public bool pauseTravel = false;

    private float waitTime = -1f;
    private float delay = 0f;


    public enum State
    {
        Wait,
        Travel,
        Socialise
    }



    public void Update()
    {
        if(waitTime == -1 && !getPath)
        {
            waitTime = Random.Range(5, 30);
        }
        delay += Time.deltaTime;
        if(delay >= waitTime)
        {
            getPath = true;
            waitTime = -1;
            delay = 0;
        }


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

        if (!SelectGoal())
            return false;

        GeneratePath();

        goal = -1;
        calculating = false;

        return true;
    }



    private bool SelectGoal()
    {
        SetupMap m = GameObject.Find("Map").GetComponent<SetupMap>();
        // Get a random goal and check its not a wall. 
        goal = Random.Range(0, m.grid.Length - 1);
        if (SetupMap.nodeGraph.nodes[goal].solid == true)
        {
            calculating = false;
            return false;
        }
        else
        {
            calculating = true;
        }

        GameObject goalParent = GameObject.FindGameObjectWithTag("AllGoals");
        GameObject goalSprite = Instantiate(
            nodeSprite_debug, SetupMap.nodeGraph.nodes[goal].position, Quaternion.identity, goalParent.transform);
        goalSprite.name = "GOAL NODE " + SetupMap.nodeGraph.nodes[goal].label;
        goalSprite.GetComponent<SpriteRenderer>().color = Color.red;
        goalSprite.GetComponent<SpriteRenderer>().sortingOrder = 2;


        return true;
    }


    private void GeneratePath()
    {
        search = new Search(SetupMap.nodeGraph);
        search.Start(npcMovement.currentNode, SetupMap.nodeGraph.nodes[goal]);

        while (!search.finished)
        {
            search.Step();
        }


        /* DEBUGGGING */
        //Debug.Log("Search done. Path length " + search.path.Count + " iterations " + search.iterations);
        if (generatePath_debug)
        {
            foreach (var node in search.path)
            {
                GameObject path = Instantiate(nodeSprite_debug, node.position, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// simple travelling
    /// </summary>
    private void Travel()
    {
        if (!pauseTravel)
        {
            Movement();
        }
    }

    /// <summary>
    /// Handle the movement of the player. 
    /// </summary>
    private void Movement()
    {
        if (!initTravel)
        {
            if (!npcMovement.Init(search.path))
            {
                GotoWaitState();
                return;
            }
            else
            {
                initTravel = true;
            }
        }
        else
        {
            if (npcMovement.Move())
            {
                GotoWaitState();
                return;
            }
        }
    }



    private void GotoWaitState()
    {
        initTravel = false;
        currentState = State.Wait;
    }



    private void Socialise()
    {
    }
}