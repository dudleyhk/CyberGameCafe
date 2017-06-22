using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCMovement : MonoBehaviour
{
    private List<Node> currentPath = null;
    private Node currentNode = null;
    private Transform tranform = null;
    public  Vector3 currentDir = Vector3.zero;

    public static Direction CurrentDirection { get; internal set; }


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


    // This script wont be a singleton later in developemtn
    private static NPCMovement _instance = null;
    public static NPCMovement Instance
    {
        get
        {
            if (!_instance)
            {
                var mover = FindObjectOfType<NPCMovement>();
                _instance = mover;
            }
            return _instance;
        }
    }
    private void Awake()
    {
        tranform = this.transform;
    }

    public void BeingTravels(List<Node> path)
    {
         currentPath = path;

        currentNode = currentPath[0];

        // FOR DEBUGGING this could be used for all objects when the game is loading to snap all characters to the closest Node.Centre
        tranform.position = currentNode.Centre;

        StartCoroutine(CompletePath());

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
        if(currentNode.Centre.x > nextNode.Centre.x)
        {
            leftFlag = true;
            direction = Direction.LEFT;
        }
        else if(currentNode.Centre.x < nextNode.Centre.x)
        {
            rightFlag = true;
            direction = Direction.RIGHT;
        }


        if (currentNode.Centre.y > nextNode.Centre.y)
        {
            downFlag = true;
            direction = Direction.DOWN;
        }
        else if (currentNode.Centre.y < nextNode.Centre.y)
        {
            upFlag = true;
            direction = Direction.UP;
        }

        if(upFlag)
        {
            if(rightFlag)
            {
                direction = Direction.UP_RIGHT; 
            }

            if(leftFlag)
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
            
            if(leftFlag)
            {
                direction = Direction.DOWN_LEFT;
            }
        }
        return direction;
    }


    private void SetDirectionVec(Direction dir)
    {
        float nodeWidth = GridManager.Instance.nodeWidth;
        float nodeHeight = GridManager.Instance.nodeHeight;

        Vector3 up    = new Vector3(0f, nodeHeight,  0f);
        Vector3 down  = new Vector3(0f, -nodeHeight, 0f);
        Vector3 right = new Vector3(nodeWidth,  0f, 0f);
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


    private IEnumerator CompletePath()
    {
        int idx = 0;
        //Debug.Log("path size: " + currentPath.Count);
        while(currentNode.ID != currentPath[currentPath.Count - 1].ID)
        {
            if ((idx + 1) < currentPath.Count)
            {
                //Debug.Log("IDX + 1 or " + (idx + 1) + " is less than " + currentPath.Count);
                CurrentDirection = GetDirection(currentPath[idx + 1]);
                SetDirectionVec(CurrentDirection);

               // Debug.Log("Is " + this.tranform.position + " == " + currentPath[idx + 1].Centre + "?");
                if ((Mathf.Abs(this.tranform.position.x - currentPath[idx + 1].Centre.x) <= 0.05f) &&
                    (Mathf.Abs(this.tranform.position.y - currentPath[idx + 1].Centre.y) <= 0.05f))
                {
                    idx++;
                   // Debug.Log("Current path index " + idx);
                }
            }

            transform.position += currentDir * Time.deltaTime;
            currentNode = currentPath[idx];
            //Debug.Log("From Node " + currentNode.ID + " to node " + currentPath[idx + 1].ID + " the current direction is " + CurrentDirection);
           // Debug.Log("Moving vector position: " + tranform.position);
            yield return null;
        } 
        yield return true;
    }
}
