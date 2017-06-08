using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();







    // It might be better to already break the map into squares and use List.Contains to 
    //   get the element which is being checked.
    public void Search(Vector2 startPosition)
    {
        // Add the current Node to the list with a default weight of 0.0f
        Node start = new Node(startPosition, null);
        openList.Add(start);

        // Add the 8 adjascent Nodes to the list. Each with the parent node.
        // Dont add it to the list if it has a weight > x
        openList.Add(new Node(startPosition + Vector2.up, start));                
        openList.Add(new Node(startPosition + Vector2.down, start));
        openList.Add(new Node(startPosition + Vector2.left, start));
        openList.Add(new Node(startPosition + Vector2.right, start));
        openList.Add(new Node(startPosition + Vector2.left + Vector2.up, start));
        openList.Add(new Node(startPosition + Vector2.right + Vector2.up, start));
        openList.Add(new Node(startPosition + Vector2.left + Vector2.down, start));
        openList.Add(new Node(startPosition + Vector2.right + Vector2.down, start));
    }


    private void AddAdjascentNodes(Node baseNode)
    {
        // If the adjascent node being checked has a weight of -1;
        // disregard it. 
       Node up = new Node(baseNode.Coordinates + Vector2.up, baseNode);
       if (up.Weight) openList.Add(up);

    }
}