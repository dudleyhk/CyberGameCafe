using System.Collections.Generic;
using UnityEngine;

public class Search
{
    public NodeGraph graph;
    public List<Node> reachable;
    public List<Node> explored;
    public List<Node> path;
    public Node goalNode;
    public int iterations;
    public bool finished;

    public Search(NodeGraph graph)
    {
        this.graph = graph;
    }

    /// <summary>
    /// This can be called any time. 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    public void Start(Node start, Node goal)
    {
        //ensure origin and goal are valid
        if(start.solid == true)
        {
            Debug.Log("invalid start");
            return;
        }

        if (goal.solid == true)
        {
            Debug.Log("invalid goalIdx");
            return;
        }

        //start a list of reachable nodes and put origin on it
        reachable = new List<Node>();
        reachable.Add(start);

        goalNode = goal;

        //create a list of checked nodes and a list for a valid path
        explored = new List<Node>();
        path     = new List<Node>();
        iterations = 0;

        //clear the path
        foreach (var node in graph.nodes)
        {
            node.Clear();
        }
    }

    /// <summary>
    /// This can be run at a certain point so it doesnt lock up the system. 
    /// Keep track of iterations incase you want to exit out of the search early.
    /// </summary>
    public void Step()
    {
        if (path.Count > 0)
            return;
        
        if (reachable.Count == 0)
        {
            finished = true;
            return;
        }

        iterations++;


        var node = ChoseNode();
        if(node == goalNode)
        {
            while(node != null)
            {
                path.Insert(0, node);
                node = node.previous;
            }

            GameObject.Destroy(GameObject.Find("GOAL NODE " + goalNode.label));

            finished = true;
            return;
        }

        reachable.Remove(node);
        explored.Add(node);

        for(var i = 0; i < node.adjecent.Count; i++)
        {
            AddAdjacent(node, node.adjecent[i]);
        }
    }


    public void AddAdjacent(Node node, Node adjacent)
    {
        if (FindNode(adjacent, explored) || FindNode(adjacent, reachable))
            return;

        adjacent.previous = node;
        reachable.Add(adjacent);
    }


    public bool FindNode(Node node, List<Node> list)
    {
        return GetNodeIndex(node, list) >= 0;
    }



    public int GetNodeIndex(Node node, List<Node> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (node == list[i])
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Randomly select a goalIdx node. 
    /// </summary>
    /// <returns></returns>
    public Node ChoseNode()
    {
        return reachable[Random.Range(0, reachable.Count)];
    }
}
