using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();
    private Node currentNode = null;
    private int targetNodeID = 0;
    private ushort nodesAcross = 0;
    private ushort nodesUp = 0;
	public int  diagonalCost = 14;
	public int  orthogonalCost = 10;
    public bool routeFound = false;




    private static AStar _instance = null;
    public static AStar Instance
    {
        get
        {
            if(!_instance)
            {
                var aStar = FindObjectOfType<AStar>();
                _instance = aStar;
            }
            return _instance;
        }
    }



    // http://www.policyalmanac.org/games/aStarTutorial.htm

    // F = G + H 
    // H : value for the DISTANCE a node is from the end (this includes weighted nodes)
    // G : The cost value it takes to get to this node (in the example diagonals cost slightly more).
    private void Awake()
    {
        nodesAcross = GridManager.Instance.GetNodesAcross();
        nodesUp     = GridManager.Instance.GetNodesUp();
        
    }


    public bool Search(int nodeID, int targetID)
    {
        bool exitFlag = false; 

        currentNode = GridManager.Instance.GetNode(nodeID);
        targetNodeID = targetID;

        if (currentNode.Equals(null) || GridManager.Instance.GetNode(targetNodeID).Equals(null))
        {
            Debug.Log("Start or Target Node ID invalid");
            return false;
        }
        openList.Add(currentNode);

        StartCoroutine(SearchLoop(currentNode, exitFlag));
        if(exitFlag)
        {
            return false;
        }        return true;
    }

    private IEnumerator SearchLoop(Node node, bool exitFlag)
    {
        int iter = 0;
        while (!routeFound)
        {
            SelectNewParent();
            AddToClosedList(currentNode);
            CheckSurroundingNodes(currentNode);

            if (!CheckForPath())
            {
                Debug.Log("Path not found.");
                exitFlag = false;
            }

            Debug.Log("Loop cycle " + iter++);
        }
        Debug.Log("Exit Loop");
        yield break;
    }

    private void CheckSurroundingNodes(Node node)
    {
        List<KeyValuePair<int, int>> adjascentNodes = GetAdjascentIDs(node.ID);
        foreach(KeyValuePair<int, int> n in adjascentNodes)
        {
            Node adjascentNode = GridManager.Instance.GetNode(n.Key);
            if(adjascentNode.Equals(null))
            {
                Debug.Log("ID " + n.Key + " isn't valid.. Skipping.");
                continue;
            }

            // check if its walkable or on the closed list. 
            if (adjascentNode.Weight != GridManager.SpriteWeight.None || closedList.Contains(adjascentNode))
                continue;

            // if its not on the openList,
            if (!openList.Contains(adjascentNode))
            {
                //  make the currentNode its parent. Calc G, H and F for this. 
                adjascentNode.Parent = currentNode;
                SetCostAndDistance(adjascentNode, n.Value);
                openList.Add(adjascentNode);

            }
            else
            {
                // Check to see if changing the parent would create a better path.
                int testCost = n.Value + node.Cost;
                if(testCost < node.Cost)
                {
                    // Change the parent and re-calc Cost and Dist. 
                    adjascentNode.Parent = currentNode;
                    SetCostAndDistance(adjascentNode, n.Value);
                }
            }
        }




        /*
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
        */
    }


    ///// <summary>
    ///// Check the weight to see if its covered by a sprite and check if a parent node 
    /////     is passed in. 
    ///// </summary>
    ///// <param name="ID"></param>
    //private static void AddToOpenList(int ID, Node parentNode, uint cost)
    //{
    //    Node node = GridManager.Instance.GetNode(ID);

    //    if (node.Equals(null))
    //    {
    //        Debug.Log("Invalid node aquired");
    //        return;
    //    }

    //    if (parentNode.Equals(null))
    //    {
    //        Debug.Log("Parent Node passed in as NULL");
    //        return;
    //    }
    //    openList.Add(node);
    //}


    private List<KeyValuePair<int, int>> GetAdjascentIDs(int parentID)
    {
        List<KeyValuePair<int, int>> IDList = new List<KeyValuePair<int, int>>();
        int ID = currentNode.ID;

        bool upFlag = false;
        bool downFlag = false;
        bool leftFlag = false;
        bool rightFlag = false;



        if (ID <= (nodesAcross * nodesUp) - nodesAcross)
        {
            IDList.Add(new KeyValuePair<int,int>(ID + nodesAcross, orthogonalCost)); // UP
            upFlag = true;
        }

        if (ID > nodesAcross)
        {
            IDList.Add(new KeyValuePair<int, int>(ID - nodesAcross, orthogonalCost)); // DOWN
            downFlag = true;
        }

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
        if (node.Equals(null))        return;
        if (!openList.Contains(node)) return;

        openList.Remove(node);
        closedList.Add(node);
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
        int targetNodeY  = (targetNodeID / nodesAcross);
        int targetNodeX  = (targetNodeID % nodesAcross);
        
        int differenceX = Mathf.Abs(targetNodeX - currentNodeX);
        int differenceY = Mathf.Abs(targetNodeY - currentNodeY);
        int distanceCost = differenceX + differenceY;

        node.Cost     = currentNode.Cost + cost;
        node.Distance = distanceCost;

        node.TotalValue = cost + distanceCost;
    }


    private void SelectNewParent()
    {
        Node parentNode = openList[0];
        
        // Find node with the lowest MoveCost
        foreach (Node node in openList)
        {
            if(node.TotalValue < parentNode.TotalValue)
            {
                parentNode = node;
            }
        }

        Debug.Log("New parent MoveCost: " + parentNode.TotalValue);
    }



    private bool CheckForPath()
    {
        foreach (Node node in closedList)
        {
            // check if target has been added to the closed list. 
            if (node.ID == targetNodeID)
            {
                routeFound = true;
                break;
            }
        }

        //// check if openlist is empty and there is no target in the closed list. 
        //if (openList.Count <= 0)
        //{
        //    if (!closedList.Contains(targetNode))
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
}