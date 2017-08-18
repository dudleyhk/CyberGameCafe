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

    public Node currentNode = null;



    private void Awake()
    {
        speed = 1.5f;
    }

    /// <summary>
    /// if the path being passed in has nothing in it. Bump out back to wait state to get another path. 
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    public bool Init(List<Node> _path)
    {
        if (_path.Count <= 0)
        {
            Debug.Log("Path count < 0");
            return false;
        }
        path     = new List<Node>(_path);
        currentIdx = 0;
        goalIdx    = path.Count;


        print("Current: " + currentIdx);
        print("Goal: " + goalIdx);
        playerTransform.position = path[currentIdx].position;

        return true;
    }


    public bool Move()
    {
        if (path == null || path.Count <= 0)
        {
            Debug.Log("Error with Path in NPCMovement.Move()... ABORT!");
            if (currentIdx > path.Count || currentIdx < 0)
            {
                Debug.Log("Error with currentIdx in NPCMovement.Move()... ABORT!");
                Debug.Log("CurrentIdx is " + currentIdx);
            }
            return true;
        }
        currentNode = path[currentIdx];
        if (playerTransform.position == path[currentIdx].position)
        {
            //print("Incrmenting current node");
            currentIdx++;

            if (currentIdx >= goalIdx)
            {
                //print("current value is more than path length");
                print("goal reached");
                
                goalIdx = -1;
                return true;
            }
        }
        playerTransform.position = Vector3.MoveTowards(
            playerTransform.position,
            path[currentIdx].position,
            speed * Time.deltaTime);

        return false;
    }
}
