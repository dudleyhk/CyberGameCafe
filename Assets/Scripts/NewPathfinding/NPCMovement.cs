using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public int current;
    public int goal;
    public List<Node> path;

    public float speed;


	public void Init(List<Node> _path)
    {
        path  = new List<Node>(_path);
        current = 0;
        goal = path.Count - 1;


        print("Current: " + current);
        print("Goal: " + goal);
        transform.position = path[current].position;


    }


    public IEnumerator Move()
    {
        if (path == null)
        {
            Debug.Log("Path is null");
            yield return false;
        }

        while (true)
        {
            if (transform.position == path[current].position)
            {
                print("Incrmenting current node");
                current++;

                if (current > goal)
                {
                    print("current value is more than path length");
                    break;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, path[current].position, speed * Time.deltaTime);
            
            yield return null;
        }
        yield return true;
    }
}
