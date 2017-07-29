using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCMovement : MonoBehaviour
{
    public Vector3 currentDir      = Vector3.zero;
    public Transform playerTransform = null;
    public bool PathComplete         = false;
    public int targetNodeID          = -1;
    public float speed  = 5f;                                  /// Speed could be a percentage of the total number of nodes their are. 
    public Direction CurrentDirection { get; internal set; }



    /* DO NOT RESET */
    public Node CurrentNode;
    public int currentNodeID;


    public bool canMove;
    public bool pathComplete;
    public int tempCurrentID;
    public Node targetNode;
    private List<Node> _path;



    public List<Node> Path
    {
        get
        {
            if(_path == null)
            {
                _path = new List<Node>();
            }
            return _path;
        }

        set
        {
            _path = value;
        }
    }
    



    public void ResetVariables()
    {
        canMove = false;
        pathComplete = false;
        tempCurrentID = 0;
        targetNode = null;
        _path = null;
    }




    public enum Direction
    {
        UP,
        UP_RIGHT,
        UP_LEFT,
        DOWN,
        DOWN_RIGHT,
        DOWN_LEFT,
        LEFT,
        RIGHT,
        NONE
    }
    

    private void Awake()
    {
        ResetVariables();
        CurrentNode = GridManager.Instance.SpawnNode;
    }



    private void Update()
    {
        if (!canMove)
            return;


        if (!targetNode)
            targetNode = Path[Path.Count - 1];


        float distToTarget = Vector3.Distance(this.transform.position, targetNode.Centre);
        if (distToTarget < 0.2f)
        { 
            Debug.Log("Path complete");
            pathComplete = true;
            return;
        }

        int nextNodeID = tempCurrentID + 1;
        if (nextNodeID < Path.Count)
        {
            print("Next node is valid");
            var nextNode = Path[nextNodeID];
            CurrentDirection = GetDirection(nextNode);
            SetDirectionVec(CurrentDirection);


            float dist = Vector3.Distance(this.transform.position, nextNode.Centre);
            if (dist < 0.2f)
            {
                tempCurrentID++;
                CurrentNode = Path[tempCurrentID];
                currentNodeID = CurrentNode.ID;
            }
        }
        print("Moving... Current Node is " + CurrentNode.ID);
        playerTransform.position += currentDir * Time.deltaTime;
    }

    public void Move()
    {
        if (Path.Count <= 0)
        {
            Debug.Log("Path empty");
            return;
        }

        if (!canMove)
        {
            canMove = true;
            print("canMove is " + canMove);
        }
    }


    public void PauseMove()
    {
        if(canMove)
            canMove = false;
        
        // dont reset path.
    }



    /// <summary>
    /// Get the direction of the next node in the list. 
    /// </summary>
    /// <param name="nextNode"></param>
    /// <returns></returns>
    private Direction GetDirection(Node nextNode)
    {
        Direction direction = Direction.NONE;
        bool upFlag = false;
        bool downFlag = false;
        bool leftFlag = false;
        bool rightFlag = false;

        //Debug.Log("current centre: " + currentNode.Centre + " next node centre: " + nextNode.Centre);
        if (CurrentNode.Centre.x > nextNode.Centre.x)
        {
            leftFlag = true;
            direction = Direction.LEFT;
        }
        else if (CurrentNode.Centre.x < nextNode.Centre.x)
        {
            rightFlag = true;
            direction = Direction.RIGHT;
        }


        if (CurrentNode.Centre.y > nextNode.Centre.y)
        {
            downFlag = true;
            direction = Direction.DOWN;
        }
        else if (CurrentNode.Centre.y < nextNode.Centre.y)
        {
            upFlag = true;
            direction = Direction.UP;
        }

        if (upFlag)
        {
            if (rightFlag)
            {
                direction = Direction.UP_RIGHT;
            }

            if (leftFlag)
            {
                direction = Direction.UP_LEFT;
            }
        }

        if (downFlag)
        {
            if (rightFlag)
            {
                direction = Direction.DOWN_RIGHT;
            }

            if (leftFlag)
            {
                direction = Direction.DOWN_LEFT;
            }
        }
        return direction;
    }


    private void SetDirectionVec(Direction dir)
    {
        float nodeWidth = GridManager.Instance.NodeWidth;
        float nodeHeight = GridManager.Instance.NodeHeight;

        Vector3 up    = new Vector3(0f, nodeHeight, 0f);
        Vector3 down  = new Vector3(0f, -nodeHeight, 0f);
        Vector3 right = new Vector3(nodeWidth, 0f, 0f);
        Vector3 left  = new Vector3(-nodeWidth, 0f, 0f);


        switch (dir)
        {
            case Direction.UP:
                currentDir = up;
                break;
            case Direction.UP_LEFT:
                currentDir = up + left;
                break;
            case Direction.UP_RIGHT:
                currentDir = up + right;
                break;
            case Direction.DOWN:
                currentDir = down;
                break;
            case Direction.DOWN_LEFT:
                currentDir = down + left;
                break;
            case Direction.DOWN_RIGHT:
                currentDir = down + right;
                break;
            case Direction.RIGHT:
                currentDir = right;
                break;
            case Direction.LEFT:
                currentDir = left;
                break;

            default:
                Debug.Log("No current direction");
                break;
        }
    }



    /// <summary>
    /// Copy the other version but change this for a FOR LOOP.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    //public IEnumerator CompletePath(List<Node> path)
    //{

    //    float distanceToNextNode = 0;

    //    path  = new List<Node>(path);
    //    CurrentNode  = path[0];
    //    targetNodeID = path[path.Count - 1].ID;


    //    int i = 0;
    //    while(i < path.Count)
    //    {
    //        int nextNodeID = i + 1;
    //        if (nextNodeID < path.Count)
    //        {
    //            Node nextNode = GridManager.Instance.GetNode(nextNodeID);
    //            CurrentDirection = GetDirection(nextNode);
    //            SetDirectionVec(CurrentDirection);


    //            distanceToNextNode = Vector3.Distance(this.transform.position, nextNode.Centre);
    //            if (distanceToNextNode < 0.2f)
    //            {
    //                print("Next target hit");
    //                i++;
    //                CurrentNode = nextNode;
    //            }
    //            print("Current Node ID: " + CurrentNode.ID);
    //            parentTransform.position += currentDir * Time.deltaTime;
    //        }
    //    yield return null;  
    //    }
    //    yield return true;
    //}


        


   

    /// <summary>
    /// Run this loop until the last ID has been hit.
    /// </summary>
    /// <returns></returns>
    public IEnumerator completePath(List<Node> _path)
    {
        int idx = 0;
        Path = _path;
        CurrentNode = Path[0];
        int targetID = Path[Path.Count - 1].ID;
        PathComplete = false;


        while(CurrentNode.ID != targetID)
        {
            //Debug.Log("WHILE CurrentNode.ID (" + CurrentNode.ID + ") != targetID (" + targetID + ") is " + (CurrentNode.ID != targetID)); 
            //Debug.Log("idx (" + idx + ") < currentPath.Count (" + currentPath.Count + ") is " + ((idx + 1) < currentPath.Count));

            // If the next index is less that the current _path size. 
            if ((idx + 1) < Path.Count)
            {
                Node nextNode = Path[idx + 1];
                CurrentDirection = GetDirection(nextNode);
                SetDirectionVec(CurrentDirection);

                float dist = Vector3.Distance(this.transform.position, nextNode.Centre);
                //Debug.Log("Distance: " + dist);
                if (dist < 0.2f)
                {
                   // Debug.Log("Increment the idx");
                    idx++;
                }

                //if ((Mathf.Abs(parentTransform.position.x - nextNode.Centre.x) <= 0.05f) &&
                //    (Mathf.Abs(parentTransform.position.y - nextNode.Centre.y) <= 0.05f))
                //{
                //    idx++;
                //}
            }


            // Move
            playerTransform.position += currentDir * Time.deltaTime;
            CurrentNode = Path[idx];

            yield return null;
        }
      //  Debug.Log("WHILE CurrentNode.ID (" + CurrentNode.ID + ") != targetID (" + targetID + ") is " + (CurrentNode.ID != targetID));
      //  Debug.Log("idx (" + idx + ") < currentPath.Count (" + currentPath.Count + ") is " + ((idx + 1) < currentPath.Count));
        PathComplete = true;
        yield return true;
    }


    /// <summary>
    /// Has the character got to the final targetNode. 
    /// </summary>
    /// <returns></returns>
    public bool JourneyComplete()
    {
        if(PathComplete)
        {
            PathComplete = false;
            return true;
        }
        return false;
    }



    /// <summary>
    /// Pass in the next node and check if it is occupied. 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool NodeOccupied(Node node)
    {
        if(node.Occupied)
        {
            // either, change the current state to wait and disregard this _path. Or Wait for X seconds before continuing.
        }
        return false;
    }
}
