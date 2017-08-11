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
        currentNode = SetupMap.nodeGraph.nodes[51];//spawnNode;   
    }


    public void Init(List<Node> _path)
    {
        path     = new List<Node>(_path);
        currentIdx = 0;
        goalIdx    = path.Count;


        print("Current: " + currentIdx);
        print("Goal: " + goalIdx);
        playerTransform.position = path[currentIdx].position;


    }


    public bool Move()
    {
        if (path == null)
        {
            Debug.Log("Path is null");
            return false;
        }

        // set the current node id player is on. 
        currentNode = path[currentIdx];
        if (playerTransform.position == path[currentIdx].position)
        {
            print("Incrmenting current node");
            currentIdx++;

            if (currentIdx >= goalIdx)
            {
                print("current value is more than path length");
                print("goal reached");
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
