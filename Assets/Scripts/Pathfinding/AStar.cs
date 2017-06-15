using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private static List<Node> openList = new List<Node>();
    private static List<Node> closedList = new List<Node>();
    private static ushort nodesAcross = 0;
    private static ushort nodesUp = 0;
	
	public static uint  diagonalCost = 14;
	public static uint  orthogonalCost = 10;
    public static uint  targetNodeID = 0;


    // http://www.policyalmanac.org/games/aStarTutorial.htm

    // F = G + H 
    // H : value for the DISTANCE a node is from the end (this includes weighted nodes)
    // G : The cost value it takes to get to this node (in the example diagonals cost slightly more).
    private void Awake()
    {
        nodesAcross = GridManager.Instance.GetNodesAcross();
        nodesUp     = GridManager.Instance.GetNodesUp();
    }


    public static bool Search(uint nodeID, uint targetID)
    {
        Node startNode = GridManager.Instance.GetNode(nodeID);

        if (startNode.Equals(null))
        {
            Debug.Log("Node ID invalid");
            return false;
        }
        targetNodeID = targetID;
        openList.Add(startNode);

        AddAdjascentNodes(startNode);
        AddToClosedList(startNode);

        return true;
    }


    private static void AddAdjascentNodes(Node parentNode)
    {
        int ID = parentNode.ID;
        int up    = -1;
        int down  = -1;
        int left  = -1;
        int right = -1;
        int upLeft    = -1;
        int upRight   = -1;
        int downLeft  = -1;
        int downRight = -1;


		//Debug.Log("UP: " + ( ID <= (nodesAcross * nodesUp) - nodesAcross));
        if (ID <= (nodesAcross * nodesUp) - nodesAcross)
        {
            up = ID + nodesAcross;
            AddToOpenList(up, parentNode, orthogonalCost);
        }

		//Debug.Log("DOWN: " + (ID > nodesAcross));
        if (ID > nodesAcross)
        {
            down = ID - nodesAcross;
            AddToOpenList(down, parentNode, orthogonalCost);
        }

       // Debug.Log("Left: " + (ID % nodesAcross > 0));
        if (ID % nodesAcross > 0)
        {
            left = ID - 1;
           AddToOpenList(left, parentNode, orthogonalCost);
        }
        

        int y = (ID / nodesUp);
        //Debug.Log("RIGHT: " + (ID != (y * nodesAcross) + (nodesAcross - 1)));
        if (ID != (y * nodesAcross) + (nodesAcross - 1))
        {
            right = ID + 1;
            AddToOpenList(right, parentNode, diagonalCost);
        }


        if (up > -1)
        {
            if(left > -1)
            {
                upLeft = up - 1;
                AddToOpenList(upLeft, parentNode, diagonalCost);

            }
            if(right > -1)
            {
                upRight = up + 1;
                AddToOpenList(upRight, parentNode, diagonalCost);
            }
        }

        if(down > -1)
        {
            if (left > -1)
            {
                downLeft = down - 1;
                AddToOpenList(downLeft, parentNode, diagonalCost);

            }
            if(right > -1)
            {
                downRight = down + 1;
                AddToOpenList(downRight, parentNode, diagonalCost);
            }
        }




     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)upLeft).Centre, 1);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)up).Centre, 2);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)upRight).Centre, 3);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)left).Centre, 4);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)right).Centre, 5);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)downLeft).Centre, 6);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)down).Centre, 7);
     // Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)downRight).Centre, 8);


      //Debug.Log("BASEID: "    + ID);
      //Debug.Log("UP: "        + up);
      //Debug.Log("DOWN: "      + down);
      //Debug.Log("LEFT: "      + left);
      //Debug.Log("RIGHT: "     + right);
      //Debug.Log("UPRIGHT: "   + upRight);
      //Debug.Log("UPLEFT: "    + upLeft);
      //Debug.Log("DOWNRIGHT: " + downRight);
      //Debug.Log("DOWNLEFT: "  + downLeft);
    }


    /// <summary>
    /// Check the weight to see if its covered by a sprite and check if a parent node 
    ///     is passed in. 
    /// </summary>
    /// <param name="ID"></param>
    private static void AddToOpenList(int ID, Node parentNode, uint cost)
    {
        uint uID = (uint)ID;

        Node node = GridManager.Instance.GetNode(uID);
        if (node.Equals(null)) return;

        SetCostAndDistance(node, cost);

        if (node.Weight > 0)
        {
            if(!parentNode.Equals(null))
            {
                node.Parent = parentNode;
            }
            openList.Add(node);
        }
    }

    /// <summary>
    /// Sanity check the node being passed in and remove it from the openlist and 
    ///    add it to the closed list. 
    /// </summary>
    /// <param name="node"></param>
    private static void AddToClosedList(Node node)
    {
        if (node.Equals(null))        return;
        if (!openList.Contains(node)) return;

        openList.Remove(node);
        closedList.Add(node);
    }


    private static void SetCostAndDistance(Node currentNode, uint cost)
    {
        int distanceCost = 0;

        // Calculate how many nodes away we are from the target
        distanceCost = currentNode.ID - (int)targetNodeID;    /// THIS NEEDS TO BE SLGHTLY CLEVERER. AT THE MOMENT IT RUNS THROUGH EACH NUMBER WHERE AS WE NEED TO SEE HOW MANY ROW/COLUMNS IT IS AWAY 
        if(distanceCost < 0)
        {
            // multiple by -1 to get the positive version.
           distanceCost = distanceCost * -1;
        }

        currentNode.Cost = cost;
        currentNode.DistanceToTarget = (uint)distanceCost;
    }
}