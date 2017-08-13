using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the Waiting State
/// </summary>
public class NPCBehaviour : MonoBehaviour
{
    /* General */
    public State currentState = State.Wait;

    /*Waiting */
    public GameObject nodeSprite_debug = null;
    public bool calculating = false;
    public bool getPath = false;
    public int goal = -1;
    public Search search;
    public bool generatePath_debug = false;
    public int maxIterations = 1600;
    public bool initPathFinding = false;
    public int waitTime = -1;
    
    
    /* Travel */
    public NPCMovement npcMovement = null;
    public bool initTravel = false;
    public bool pauseTravel = false;

    /* Social */
    public BoxCollider2D socialCollider = null;
    // public GameObject previousBuddy = null;
    public bool initSocialCollider = false;
    public float timer = 0f;
    public int talkTime = -1;
    public int minTalkTime = 4;
    public int maxTalkTime = 15;
    public bool talking = false; // Used by other npcs.. Do not set yourself.

    public enum State
    {
        Wait,
        Travel,
        Socialise,
        LoggedIn
    }

    private void Start()
    {
        if (!initSocialCollider)
            InitSocialCollider();

    }


    //private void FixedUpdate()
    //{
    //    switch (currentState)
    //    {
    //        case State.Wait:
    //            if (getPath)
    //            {
    //                if (!PathFinding())
    //                {
    //                    calculating = false;
    //                    getPath = true;
    //                }
    //                else
    //                {
    //                    ChangeState(State.Travel);
    //                }
    //            }
    //            break;

    //        case State.Travel:
    //            break;

    //        case State.Socialise:
    //            break;

    //        case State.LoggedIn:
    //            break;

    //        default:
    //            Debug.Log("No State selected");
    //            break;
    //    }
    //}


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
                Socialise();
                break;

            case State.LoggedIn:
                break;

            default:
                Debug.Log("No State selected");
                break;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Meeting(other.gameObject);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        talking = false;
    }


    /// <summary>
    /// Change state and reset the neccessary variables. 
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(State state)
    {
        if (state == State.Wait)
        {
            waitTime = -1;
            timer = 0f;
            initTravel = false;
            currentState = state;
        }
        else if (state == State.Travel)
        {
            getPath = false;
            initPathFinding = false;
            CharacterManager.pathFinders--;
            currentState = State.Travel;
        }
        else if (state == State.Socialise)
        {
            talkTime = -1;
            currentState = state;
        }
        else
        {
            print("Player logging in");
            currentState = state;
        }
    }


    /// <summary>
    /// Basic waiting functionality. Pick a random target and find the _path for it. 
    /// </summary>
    private void Waiting()
    {
        // Stop the character. 
        if (waitTime < 0)
            waitTime = RandomNumber(2, 15);

        if (timer <= waitTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!initPathFinding)
            {
                if (InitPathFinding())
                {
                    initPathFinding = true;
                }
                else
                {
                    initPathFinding = false;
                    return;
                }
            }
            // Pathfinding
            if (getPath)
            {
                if (!PathFinding())
                {
                    calculating = false;
                    getPath = true;
                }
                else
                {
                    ChangeState(State.Travel);
                }
            }
        }
    }

    private int RandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }


    private bool InitPathFinding()
    {
        if (!CharacterManager.pathFindingLocked)
        {
            getPath = true;
            CharacterManager.pathFinders++;
            //print("Increment pathfinfing");
            //print("Number of pathfinders : " + CharacterManager.pathFinders);
            return true;
        }
        else
        {
            return false;
        }
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

        if (!GeneratePath())
            return false;

        goal = -1;
        calculating = false;

        return true;
    }



    private bool SelectGoal()
    {
        // Get a random goal and check its not a wall. 
        goal = Random.Range(0, SetupMap.nodeGraph.nodes.Length - 1);
        if (SetupMap.nodeGraph.nodes[goal].solid)
        {
            return false;
        }
        else
        {
            calculating = true;
        }


        // GameObject goalSprite = Instantiate(nodeSprite_debug, setupMap.nodeGraph.nodes[goal].position, Quaternion.identity);
        // goalSprite.name = "GOAL NODE " + setupMap.nodeGraph.nodes[goal].label;
        // goalSprite.GetComponent<SpriteRenderer>().color = Color.red;
        // goalSprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
        // 
        return true;
    }


    private bool GeneratePath()
    {
        search = new Search(SetupMap.nodeGraph);
        search.Start(npcMovement.currentNode, SetupMap.nodeGraph.nodes[goal]);

        while (!search.finished)
        {
            search.Step();

            if (search.iterations > maxIterations)
            {
                // print("max iters hit");
                return false;
            }
        }
        return true;
    }



    /// <summary>
    /// simple travelling
    /// </summary>
    private void Travel()
    {
        Movement();
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
                ChangeState(State.Wait);
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
                ChangeState(State.Wait);
                return;
            }
        }
    }



    private void Socialise()
    {
        ChangeState(State.Wait);

        //if (!talking)
        //    ChangeState(State.Wait);

        //if(talkTime == -1)
        //    talkTime = Random.Range(minTalkTime, maxTalkTime);


        //// Start Timer
        //timer += Time.deltaTime;
        //if (timer >= talkTime)
        //{
        //    previousBuddy.GetComponentInChildren<NPCBehaviour>().talking = false;
        //    ChangeState(State.Wait);
        //}


        // Run talking animations
        //Debug.Log("TALK TALK");

    }


    private void InitSocialCollider()
    {
        var w = MapData.nodeWidth;
        var h = MapData.nodeHeight;

        // Multiply by 2 because social box has a reach of upto 2 nodes away in each direction. 
        var boxSize = new Vector2(w, h);
        boxSize = boxSize * 2;
        socialCollider.size = boxSize;

        initSocialCollider = true;
    }


    /// <summary>
    /// 
    /// </summary>
    private void Meeting(GameObject gameObject)
    {
        //print(gameObject.name + " has entered my trigger collider");

        var npcBehaviour = gameObject.GetComponentInChildren<NPCBehaviour>();
        if (!npcBehaviour)
            return;

        if (npcBehaviour.currentState == State.LoggedIn)
            return;

        if (npcBehaviour.talking)
            return;

        npcBehaviour.talking = true;

        ChangeState(State.Socialise);
    }
}