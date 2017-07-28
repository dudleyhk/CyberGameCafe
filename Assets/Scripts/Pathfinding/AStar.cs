using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public  List<Node> _openList   = null;
    public  List<Node> _closedList = null;
    public  List<Node> _path       = null;

    private ushort nodesAcross = 0;
    private ushort nodesUp = 0;
	public int  diagonalCost = 14;
	public int  orthogonalCost = 10;


    public bool isActive;
    public bool pathAquired;
    public Node targetNode;


    public List<Node> ClosedList
    {
        get
        {
            if (_closedList == null)
                _closedList = new List<Node>();
            return _closedList;
        }
    }


    public List<Node> OpenList
    {
        get
        {
            if (_openList == null)
                _openList = new List<Node>();
            return _openList;
        }
    }

    public List<Node> Path
    {
        get
        {
            if (_path == null)
                _path = new List<Node>();
            return _path;
        }
    }






    public void ResetVariables()
    {
        _openList   = null;
        _closedList = null;
        _path       = null;

        targetNode = null;

        isActive = false;
        pathAquired = false;
    }
   
   
    // http://www.policyalmanac.org/games/aStarTutorial.htm

    // F = G + H 
    // H : value for the DISTANCE a node is from the end (this includes weighted nodes)
    // G : The cost value it takes to get to this node (in the example diagonals cost slightly more).
    private void Awake()
    {
        ResetVariables();

        nodesAcross = GridManager.Instance.NodesAcross;
        nodesUp     = GridManager.Instance.NodesUp;
    }



    // New function which sets the isActive bool to true.
    // Also, return T or F if the Path can/cannot be found.  
    public void StartPathFinding(int startID)
    {
        if (isActive)
            return;

        var startNode = GridManager.Instance.GetNode(startID);
        Debug.Log("Start node id: " + startID);
        

        isActive = true;
        Debug.Log("isActive: " + isActive);
        OpenList.Add(startNode);
    }


    public bool ValidateTarget(int targetID)
    {
        targetNode = GridManager.Instance.GetNode(targetID);
        Debug.Log("Target node id: " + targetID);
        

        if(!targetNode)
        {
            Debug.Log("Target Node ID invalid");
            return false;
        }
        if (targetNode.Weight == GridManager.SpriteWeight.Static)
        {
            Debug.Log("Target is covered");
            return false;
        }
        return true;
    }



    private void Update()
    {
        if (!isActive)
            return;


        var currentNode = SelectNewParent();
        AddToClosedList(currentNode);
        CheckSurroundingNodes(currentNode);

        if (CheckForPath())
        {
            pathAquired = true;
        }
    }
    


    /// <summary>
    /// For each of the adjascent nodes; check if it exists on the OpenList or not. 
    /// </summary>
    /// <param name="node"></param>
    private void CheckSurroundingNodes(Node node)
    {
        List<KeyValuePair<int, int>> adjascentNodes = GetAdjascentIDs(node.ID);

        foreach(KeyValuePair<int, int> n in adjascentNodes)
        {
            Node adjascentNode = GridManager.Instance.GetNode(n.Key);
            if(adjascentNode == null)
                continue;
            

            // check if its walkable or on the closed list. 
            if (adjascentNode.Weight != GridManager.SpriteWeight.None || ClosedList.Contains(adjascentNode))
                continue;

            // if its not on the _openList,
            if (!OpenList.Contains(adjascentNode))
            {
                //  make the currentNode its parent. Calc G, H and F for this. 
                adjascentNode.Parent = node;
                SetCostAndDistance(adjascentNode, n.Value);
                OpenList.Add(adjascentNode);
            }
            else
            {
                // Add the parent movecost to the potencial move cost if this direction was used. 
                int testCost = node.Cost + n.Value;
                int currentCost = node.Cost + adjascentNode.Cost;

                // if the test cost is less than the current adjascent movecost
                if(testCost < currentCost)
                {
                    // Change the parent and re-calc Cost and Dist. 
                    adjascentNode.Parent = node;
                    adjascentNode.Cost = testCost;
                    adjascentNode.TotalValue = adjascentNode.Distance + adjascentNode.Cost;
                }
            }
        }
    }


    /// <summary>
    /// Find each of the adjascent nodes and return  all of their IDs and MoveCost
    ///     as a list.
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    private List<KeyValuePair<int, int>> GetAdjascentIDs(int parentID)
    {
        List<KeyValuePair<int, int>> IDList = new List<KeyValuePair<int, int>>();
        int ID = parentID;

        bool upFlag = false;
        bool downFlag = false;
        bool leftFlag = false;
        bool rightFlag = false;


        if (ID < (nodesAcross * nodesUp) - nodesAcross)
        {
            IDList.Add(new KeyValuePair<int,int>(ID + nodesAcross, orthogonalCost)); // UP
            upFlag = true;
        }

        if (ID > nodesAcross)
        {
            IDList.Add(new KeyValuePair<int, int>(ID - nodesAcross, orthogonalCost)); // DOWN
            downFlag = true;
        }

       // Debug.Log("Nodes Across: " + nodesAcross);
        if (ID % nodesAcross > 0)
        {
            IDList.Add(new KeyValuePair<int, int>(ID - 1, orthogonalCost));  // left
            leftFlag = true;
        }


        int y = (ID / nodesUp);
        if (ID != (y * nodesAcross) + (nodesAcross - 1))
        {
            IDList.Add(new KeyValuePair<int, int>(ID + 1, orthogonalCost)); // Right
            rightFlag = true;
        }


        if (upFlag)
        {
            if (leftFlag)
            {
                IDList.Add(new KeyValuePair<int, int>((ID + nodesAcross) - 1, diagonalCost)); // UP_LEFT

            }
            if (rightFlag)
            {
                IDList.Add(new KeyValuePair<int, int>((ID + nodesAcross) + 1, diagonalCost)); // UP_RIGHT
            }
        }

        if (downFlag)
        {
            if (leftFlag)
            {
                IDList.Add(new KeyValuePair<int, int>((ID - nodesAcross) - 1, diagonalCost)); // DOWN_LEFT

            }
            if (rightFlag)
            {
                IDList.Add(new KeyValuePair<int, int>((ID - nodesAcross) + 1, diagonalCost)); // DOWN_RIGHT
            }
        }
        return IDList;
    }

    /// <summary>
    /// Sanity check the node being passed in and remove it from the openlist and 
    ///    add it to the closed list. 
    /// </summary>
    /// <param name="node"></param>
    private void AddToClosedList(Node node)
    {
        if (!node)                    return;
        if (!OpenList.Contains(node)) return;

        OpenList.Remove(node);
        ClosedList.Add(node);
    }

    /// <summary>
    /// Calculate the distance from the target node by subtracting the targetNode x and y 
    ///     from the currentNode x and y. Use these values to set the final Movement cost of the
    ///     Node.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="cost"></param>
    private void SetCostAndDistance(Node node, int cost)
    {                
        int currentNodeY = (node.ID / nodesAcross);
        int currentNodeX = (node.ID % nodesAcross);
        int targetNodeY  = (targetNode.ID / nodesAcross);
        int targetNodeX  = (targetNode.ID % nodesAcross);
        
        int differenceX = Mathf.Abs(targetNodeX - currentNodeX);
        int differenceY = Mathf.Abs(targetNodeY - currentNodeY);
        int distanceCost = differenceX + differenceY;

        node.Cost     = node.Parent.Cost + cost;
        node.Distance = distanceCost;

        node.TotalValue = node.Cost + node.Distance;
    }


    /// <summary>
    /// Find the node with the lowest TotalValue and set it as the new parent node.
    /// </summary>
    /// <returns></returns>
    private Node SelectNewParent()
    {
        int minTotalValue = OpenList[0].TotalValue;
        Node parent = null;

        foreach (var node in OpenList)
        {
            if(node.TotalValue <= minTotalValue)
            {
                minTotalValue = node.TotalValue;
                parent = node;
            }
        }
        return parent;
    }


    /// <summary>
    /// Check for a _path in the Closed list and check if a route cannot be found. 
    /// </summary>
    /// <returns></returns>
    private bool CheckForPath()
    {
        Node endNode = ClosedList.Find(node => { return node.ID == targetNode.ID; });

        if(endNode)
        {
            CreatePath(endNode);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Find the _path starting at the targetNode. Add each element starting at the 
    ///     startNode to the PathList. 
    /// </summary>
    /// <param name="targetNode"></param>
    private void CreatePath(Node targetNode)
    {
        Node node = targetNode;
        
        for(int i = 0; i < ClosedList.Count; i++)
        {
            Path.Add(node);

           if (node.Parent == null)
             break;

            node = node.Parent;
        }

        Path.Reverse();

        for (int i = 0; i < Path.Count; i++)
        {
            Debugging.Instance.PlaceDebugCube(Path[i].Centre, Path[i].ID);
            Debug.Log("Path " + i + " is " + Path[i].ID);
        }
    }
}