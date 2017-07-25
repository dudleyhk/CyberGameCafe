using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCMovement : MonoBehaviour
{
    private List<Node> currentPath = null;
    public Vector3 currentDir = Vector3.zero;
    public Node _currentNode = null;
    public Transform parentTransform = null;
    public int  currentNodeID = -1;
    public bool pathComplete = false;
    public Direction CurrentDirection { get; internal set; }


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



    public Node CurrentNode
    {
        get
        {
            if(_currentNode == null)
            {
                _currentNode = GridManager.Instance.GetNode(currentNodeID);
            }
            return _currentNode;
        }

        set
        {
            _currentNode = value;
            currentNodeID = _currentNode.ID;
        }
    }

    

    private void Awake()
    {
        currentNodeID = GridManager.Instance.spawnNodeID;
    }



    public bool JourneyToTarget(List<Node> path)
    {
        StartCoroutine(CompletePath(path));

        // FOR DEBUGGING this could be used for all objects when the game is loading to snap all characters to the closest Node.Centre
        // parentTransform.position = currentNode.Centre;

        Debug.Log("Path complete is " + pathComplete);
        if (pathComplete)
        {
            pathComplete = false;
            return true;
        }
        return false;
    }




    /// <summary>
    /// Get the direction of the next node in the list. 
    /// </summary>
    /// <param name="nextNode"></param>
    /// <returns></returns>
    private Direction GetDirection(Node nextNode)
    {
        Direction direction = Direction.NONE;


        //Debug.Log("current centre: " + currentNode.Centre + " next node centre: " + nextNode.Centre);
        if(CurrentNode.Centre.x > nextNode.Centre.x)
        {
            direction = Direction.LEFT;
        }
        else if(CurrentNode.Centre.x < nextNode.Centre.x)
        {
            direction = Direction.RIGHT;
        }


        if (CurrentNode.Centre.y > nextNode.Centre.y)
        {
            direction = Direction.DOWN;
        }
        else if (CurrentNode.Centre.y < nextNode.Centre.y)
        {
            direction = Direction.UP;
        }

        return direction;
    }



    /// <summary>
    /// Use the current Direction to create a new Vector3 Direction. 
    /// </summary>
    /// <param name="dir"></param>
    private void SetDirectionVec(Direction dir)
    {
        switch (dir)
        {
            case Direction.UP:
                currentDir = Vector3.up;
                break;
            case Direction.DOWN:
                currentDir = Vector3.down;
                break;
            case Direction.RIGHT:
                currentDir = Vector3.right;
                break;
            case Direction.LEFT:
                currentDir = Vector3.left;
                break;

            default:
                Debug.Log("No current direction");
                break;
        }
    }


    /// <summary>
    /// Run this loop until the last ID has been hit.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CompletePath(List<Node> path)
    {
        int idx = 0;
        currentPath = path;
        CurrentNode = currentPath[0];
        int targetID = currentPath[currentPath.Count - 1].ID;
        Debug.Log("TARGET ID : " + targetID);


        while(CurrentNode.ID != targetID)
        {
            Debug.Log("WHILE CurrentNode.ID (" + CurrentNode.ID + ") != targetID (" + targetID + ") is " + (CurrentNode.ID != targetID)); 
            Debug.Log("idx (" + idx + ") < currentPath.Count (" + currentPath.Count + ") is " + ((idx + 1) < currentPath.Count));

            // If the next index is less that the current path size. 
            if ((idx + 1) < currentPath.Count)
            {
                Node nextNode = currentPath[idx + 1];
                CurrentDirection = GetDirection(nextNode);
                SetDirectionVec(CurrentDirection);

                float dist = Vector3.Distance(this.transform.position, nextNode.Centre);
                Debug.Log("Distance: " + dist);
                if (dist < 0.2f)
                {
                    Debug.Log("Increment the idx");
                    idx++;
                }
                     
                //if ((Mathf.Abs(parentTransform.position.x - nextNode.Centre.x) <= 0.05f) &&
                //    (Mathf.Abs(parentTransform.position.y - nextNode.Centre.y) <= 0.05f))
                //{
                //    idx++;
                //}
            }


            // Move
            parentTransform.position += currentDir * Time.deltaTime;
            CurrentNode = currentPath[idx];

            pathComplete = false;
            yield return null;
        }
        Debug.Log("WHILE CurrentNode.ID (" + CurrentNode.ID + ") != targetID (" + targetID + ") is " + (CurrentNode.ID != targetID));
        Debug.Log("idx (" + idx + ") < currentPath.Count (" + currentPath.Count + ") is " + ((idx + 1) < currentPath.Count));
        pathComplete = true;
        yield return true;
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
            // either, change the current state to wait and disregard this path. Or Wait for X seconds before continuing.
        }
        return false;
    }
}
