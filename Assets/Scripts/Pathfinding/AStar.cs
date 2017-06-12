using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private static List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();
    private static int nodesAcross = 0;
    private static int nodesUp = 0;


    // http://www.policyalmanac.org/games/aStarTutorial.htm


    private void Awake()
    {
        nodesAcross = (int)GridManager.Instance.GetNodesAcross();
        nodesUp     = (int)GridManager.Instance.GetNodesUp();
    }


    public static void Search(uint nodeID)
    {
        Node startNode = GridManager.Instance.GetNode(nodeID);
        openList.Add(startNode);

        AddAdjascentNodes(startNode);
    }


    private static void AddAdjascentNodes(Node baseNode)
    {
        int ID = baseNode.ID;
        int up    = -1;
        int down  = -1;
        int left  = -1;
        int right = -1;
        int upLeft    = -1;
        int upRight   = -1;
        int downLeft  = -1;
        int downRight = -1;


        if (ID <= (nodesAcross * nodesUp) - nodesAcross)
        {
            up = ID + nodesAcross;
            Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)up).Centre, 1);
        }

        if (ID > nodesAcross)
        {
            down = ID - nodesAcross;
            Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)down).Centre, 7);
        }

        if (ID % nodesAcross == 0)
        {
            left = ID - 1;
            Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)left).Centre, 4);
        }

        int y = (ID / nodesUp);
        if (ID == (y * nodesAcross) + (nodesAcross - 1))
        {
            right = ID + 1;
            Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)right).Centre, 5);
        }


        if(up > -1)
        {
            if(left > -1)
            {
                upLeft = up + left;
                Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)upLeft).Centre, 1);
            }
            if(right > -1)
            {
                upRight = up + right;
                Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)upRight).Centre, 3);

            }
        }

        if(down > -1)
        {
            if (left > -1)
            {
                downLeft = down + left;
                Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)downLeft).Centre, 6);
            }
            if(right > -1)
            {
                downRight = down + right;
                Debugging.Instance.PlaceDebugCube(GridManager.Instance.GetNode((uint)downRight).Centre, 8);
            }
        }
        
       
       



       Debug.Log("BASEID: "    + ID);
       Debug.Log("UP: "        + up);
       Debug.Log("DOWN: "      + down);
       Debug.Log("LEFT: "      + left);
       Debug.Log("RIGHT: "     + right);
       Debug.Log("UPRIGHT: "   + upRight);
       Debug.Log("UPLEFT: "    + upLeft);
       Debug.Log("DOWNRIGHT: " + downRight);
       Debug.Log("DOWNLEFT: "  + downLeft);
    }
}