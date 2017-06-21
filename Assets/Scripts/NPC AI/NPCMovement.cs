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

        // FOR DEBUGGING
        tranform.position = currentNode.Centre;

        // move to next node. 
        // Vector2 dir = currentNode.Centre - currentPath[1].Centre;


        CurrentDirection = GetDirection(currentPath[1]);
        Debug.Log("From Node " + currentNode.ID + " to node " + currentPath[1].ID + " the current direction is " + CurrentDirection);
        switch(CurrentDirection)
        {
            case Direction.UP:
                currentDir = Vector3.up;
                break;
            case Direction.UP_LEFT:
                currentDir = Vector3.up + Vector3.left;
                break;
            case Direction.UP_RIGHT:
                currentDir = Vector3.up + Vector3.right;
                break;
            case Direction.DOWN:
                currentDir = Vector3.down;
                break;
            case Direction.DOWN_LEFT:
                currentDir = Vector3.down + Vector3.left;
                break;
            case Direction.DOWN_RIGHT:
                currentDir = Vector3.down + Vector3.right;
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

        StartCoroutine(Move());
        

       // tranform.GetComponent<Rigidbody2D>().AddForce(currentDir * Time.deltaTime);
      //tranform.GetComponent<Rigidbody2D>().MovePosition(tranform.position + currentDir * Time.deltaTime);

    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position += currentDir * Time.deltaTime;
            Debug.Log(tranform.position);
            yield return null;
        }
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

        Debug.Log("current centre: " + currentNode.Centre + " next node centre: " + nextNode.Centre);
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

}
