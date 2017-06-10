using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Nodes are a part of the underlying invisible grid.



public class Node : MonoBehaviour
{
    public Node      ParentNode  { get; internal set; }
    public Vector2[] Coordinates { get; internal set; }
    public bool      Weight      { get; internal set; }

    // Create Node;
    //public Node(Vector2 coordinates, Node parentNode)
    //{
    //    this.ParentNode  = parentNode;
    //    this.Coordinates = coordinates;

    //    CalculateWeight();
    //}

    public Node(Vector2[] coordinates)
    {
        Coordinates = coordinates;
    }



    private void CalculateWeight()
    {
       
    }
}
