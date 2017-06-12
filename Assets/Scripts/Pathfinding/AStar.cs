using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();
    private List<Node> gridNodes = null;


    // http://www.policyalmanac.org/games/aStarTutorial.htm



    public void Search(uint nodeID)
    {
        Node startNode = GridManager.Instance.GetNode(nodeID);
        openList.Add(startNode);

        AddAdjascentNodes(startNode);
    }


    private void AddAdjascentNodes(Node baseNode)
    {
        uint ID = baseNode.ID;

        uint up = ID + GridManager.Instance.GetNodesAcross();
        uint down;
        uint left;
        uint right;
        uint upLeft;
        uint upRight;
        uint downLeft;
        uint downRight;
    }
}