using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public List<Node> adjecent = new List<Node>();
    public Node previous = null;
    public Vector3 position = Vector3.zero;
    public string label = "";
    public int type = -1;


    public void Clear()
    {
        previous = null;
    }
}
