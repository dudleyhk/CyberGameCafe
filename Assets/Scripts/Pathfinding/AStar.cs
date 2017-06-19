using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();
    private List<Node> path = new List<Node>();

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

    /// <summary>
    /// Function to start the PathFinding process
    /// </summary>
    /// <param name="nodeID"></param>
    /// <param name="targetID"></param>
    /// <returns></returns>
    public bool Search(int nodeID, int targetID)
    {
        Node startNode = GridManager.Instance.GetNode(nodeID);
        targetNodeID = targetID;

        Debug.Log("Start node id: " + nodeID);
        Debug.Log("Target node id: " + targetID);

        if (startNode.Equals(null) || GridManager.Instance.GetNode(targetNodeID).Equals(null))
        {
            Debug.Log("Start or Target Node ID invalid");
            return false;
        }
        openList.Add(startNode);
        StartCoroutine(SearchLoop());
        return true;
    }

    /// <summary>
    /// Main loop for the algorithm.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private IEnumerator SearchLoop()
    {
        int iter = 0;
        while (!routeFound)
        {
            Debug.Log("LOOP BEGIN");
            Debug.Log("Number of elements on the openList: " + openList.Count);

            Node currentNode = SelectNewParent();

            AddToClosedList(currentNode);
            CheckSurroundingNodes(currentNode);
            routeFound = CheckForPath();
            

            iter++;
            yield return null;
        }
        Debug.Log("Search complete. Number of cycles: " + iter);
        yield return true;
    }

    /// <summary>
    /// For each of the adjascent nodes; check if it exists on the OpenList or not. 
    /// </summary>
    /// <param name="node"></param>
    private void CheckSurroundingNodes(Node node)
    {
        Debug.Log("CHECKING SURROUNDING NODES");
        List<KeyValuePair<int, int>> adjascentNodes = GetAdjascentIDs(node.ID);
        Debug.Log("Parent Node " + node.ID);
        Debug.Log("Number of adjascnet nodes " + adjascentNodes.Count);

        foreach(KeyValuePair<int, int> n in adjascentNodes)
        {
            Node adjascentNode = GridManager.Instance.GetNode(n.Key);
            if(adjascentNode.Equals(null))
            {
                continue;
            }

            // check if its walkable or on the closed list. 
            if (adjascentNode.Weight != GridManager.SpriteWeight.None || closedList.Contains(adjascentNode))
                continue;

            // if its not on the openList,
            if (!openList.Contains(adjascentNode))
            {

                //  make the currentNode its parent. Calc G, H and F for this. 
                adjascentNode.Parent = node;
                SetCostAndDistance(adjascentNode, n.Value);
                openList.Add(adjascentNode);

                Debug.Log("Adding node " + adjascentNode.ID + " to openList");
                Debug.Log("Adjascent node being checked " + adjascentNode.ID + 
                    ", parentNode " + adjascentNode.Parent.ID      + 
                    ", moveCost "  + adjascentNode.Cost            + 
                    ", distance  " + adjascentNode.Distance        + 
                    " and total "  + adjascentNode.TotalValue);
            }
            else
            {
                Debug.Log("Node is added to list.. Checking for better path");
                // Check to see if changing the parent would create a better path.
                int testCost = n.Value + node.Cost;

                Debug.Log("Adjascent Value: " + n.Value);
                Debug.Log("Node Cost: " + node.Cost);
                Debug.Log("Test Value: " + testCost);
                Debug.Log("Current node cost: " + node.Cost);


                if(testCost < node.Cost)
                {
                    // Change the parent and re-calc Cost and Dist. 
                    adjascentNode.Parent = node;
                    SetCostAndDistance(adjascentNode, n.Value);


                    Debug.Log("Updating Adjascent node to better path");
                    Debug.Log("Adjascent node being checked " + adjascentNode.ID +
                        ", parentNode " + adjascentNode.Parent +
                        ", moveCost " + adjascentNode.Cost +
                        ", distance  " + adjascentNode.Distance +
                        " and total " + adjascentNode.TotalValue);
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
        Debug.Log("ADD TO THE CLOSED LIST");
        if (node.Equals(null))        return;
        if (!openList.Contains(node)) return;

        Debug.Log("Add node " + node.ID + " to the closedList");

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

        node.Cost     = node.Cost + cost;
        node.Distance = distanceCost;

        node.TotalValue = cost + distanceCost;
    }


    /// <summary>
    /// Find the node with the lowest TotalValue and set it as the new parent node.
    /// </summary>
    /// <returns></returns>
    private Node SelectNewParent()
    {
        Debug.Log("SELECT A NEW PARENT");

        // reorganise the openList so the lowest values are at the beginning. 
        openList.Sort(delegate(Node a, Node b)
        {
            return a.TotalValue.CompareTo(b.TotalValue);
        });
        return openList[0];





        //Node parentNode = openList[0];
        //// Find node with the lowest MoveCost
        //foreach (Node node in openList)
        //{
        //    Debug.Log("Total values " + node.TotalValue);
            
        //    //Debug.Log("Checking node number " + node.ID + " against current parentNode " + parentNode.ID);
        //    //if(node.TotalValue < parentNode.TotalValue)
        //    //{
        //    //    Debug.Log("Node " + node.ID + " has a TotalValue of " + node.TotalValue + " which is lower than the parents value of " + parentNode.TotalValue);
        //    //    parentNode = node;
        //    //    Debug.Log("ParentID is now: " + parentNode.ID);
        //    //}
        //}
        //return parentNode;
        ////Debug.Log("New parent ID " + parentNode.ID + " with a MoveCost: " + parentNode.TotalValue);
    }


    /// <summary>
    /// Check for a path in the Closed list and check if a route cannot be found. 
    /// </summary>
    /// <returns></returns>
    private bool CheckForPath()
    {
        Debug.Log("CHECK FOR PATH");
        Debug.Log("Number of elements in the closed list : " + closedList.Count);
        foreach (Node node in closedList)
        {
            // check if target has been added to the closed list. 
            if (node.ID == targetNodeID)
            {
                CreatePath(node);
                return true;
            }
        }

        // check if openlist is empty and there is no target in the closed list. 
        if (openList.Count <= 0 &&
            !closedList.Contains(GridManager.Instance.GetNode(targetNodeID)))
            {
                Debug.Log("Route cannot be found along this path");
                return true;
            }
        return false;
    }


    private void CreatePath(Node targetNode)
    {
        Node node = targetNode;
        path.Add(node);

        // add the target node to the list.
        while (true)
        {
            Debug.Log("Adding node " + node.ID + " to the list");

            if (node.Parent == null)
                break;

            node = node.Parent;

            if (node.Parent != null)
                Debug.Log("Node " + node.ID + " parent node is " + node.Parent.ID);
        }
    }
}