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


        reachable = new List<Node>();
        reachable.Add(start);

        goalNode = goal;

        explored = new List<Node>();
        path     = new List<Node>();
        iterations = 0;

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


    public static int GetNodeIndex(Node node, List<Node> list) { return GetNodeIndex(node, list.ToArray()); }
    public static int GetNodeIndex(Node node, Node[] list) 
    {
        for (var i = 0; i < list.Length; i++)
        {
            if (node == list[i])
                return i;
        }
        return -1;
    }


    /// <summary>
    /// Get the Global version of a node. 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static Node GetGlobalNode(Node node, Node[] list)
    {
        var index = GetNodeIndex(node, list);
        if (index != -1)
            return SetupMap.nodeGraph.nodes[index];

        return null;
    }


    public static Node GetNodeAt(int index)
    {
        if ((index < (SetupMap.nodeGraph.nodes.Length - 1)) &&
            (index > 0))
        {
            return SetupMap.nodeGraph.nodes[index];
        }
        return null;
    }



    //public static Node GetNodeAtIndex(int index, List<Node> list)
    //{
    //    for(var i = 0; i < list.Count; i++)
    //    {
    //        if(i == index)
    //            return 
    //    }
    //}

    /// <summary>
    /// Randomly select a goalIdx node. 
    /// </summary>
    /// <returns></returns>
    public Node ChoseNode()
    {
        return reachable[Random.Range(0, reachable.Count)];
    }

}
