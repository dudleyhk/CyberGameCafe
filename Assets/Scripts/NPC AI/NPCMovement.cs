using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCMovement : MonoBehaviour
{
    public Transform playerTransform;
    public int currentIdx;
    public int goalIdx;
    public List<Node> path;
    public float speed;
   // public bool pause = false;
    public bool moving = false;

    public Node previousNode = null;
    public Node currentNode = null;
    public Node nextNode = null;


    /// <summary>
    /// if the path being passed in has nothing in it. Bump out back to wait state to get another path. 
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    public bool Init(List<Node> _path)
    {
        if (_path.Count <= 0)
        {
            //Debug.Log("Path count < 0");
            return false;
        }
        path       = new List<Node>(_path);
        currentIdx = 0;
        goalIdx    = path.Count;
       // pause      = false;
        moving     = true;


        //print("Current: " + currentIdx);
        //print("Goal: " + goalIdx);
        playerTransform.position = path[currentIdx].position;

        return true;
    }

    /// <summary>
    /// Check the next node isn't occupied and move towards it. 
    /// </summary>
    /// <returns></returns>
    public bool Move()
    {
        //if (pause)
        //    return false;

        // Sanity check path. 
        if ((path == null || path.Count <= 0) ||
            (currentIdx > path.Count || currentIdx < 0))
        {
            return true;
        }

        // Set the current Node. 
        currentNode = path[currentIdx];

        // Are we at the destination?
        if (playerTransform.position == currentNode.position)
        {
            currentIdx++;
            if (currentIdx >= goalIdx)
            {
                goalIdx = -1;
                moving = false;
                return true;
            }


            previousNode = path[currentIdx - 1];
            nextNode = path[currentIdx];
        }

        // Move
        playerTransform.position = Vector3.MoveTowards(
            playerTransform.position,
            currentNode.position,
            speed * Time.deltaTime);

        return false;
    }

    /// <summary>
    /// Set the current to occupied and check if the next node is occupied. 
    /// </summary>
    /// <param name="current"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    private bool NextNodeOccupied(Node current, Node next)
    {
        current.occupied = true;
        if (next.occupied)
        {
            //print("Current Node: " + currentNodeID_debug);
            print("Next node (" + int.Parse(next.label) + ") is occupied");
            return true;
        }
        return false;
    }
}