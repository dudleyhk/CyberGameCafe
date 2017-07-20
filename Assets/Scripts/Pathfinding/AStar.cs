using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public  List<Node> openList   = new List<Node>();
    public  List<Node> closedList = new List<Node>();
    public  List<Node>  _path     = new List<Node>();

    private int targetNodeID = 0;
    private ushort nodesAcross = 0;
    private ushort nodesUp = 0;
	//public int  diagonalCost = 14;
	public int  orthogonalCost = 10;

    public bool searchInProgress = false;
    public bool initSearch = false;


    //private static AStar _instance = null;
    //public static AStar Instance
    //{
    //    get
    //    {
    //        if(!_instance)
    //        {
    //            var aStar = FindObjectOfType<AStar>();
    //            _instance = aStar;
    //        }
    //        return _instance;
    //    }
    //}

    public List<Node> Path
    {
        get
        {
            Debug.Log("AStar path has a count of " + _path.Count);
            return _path;
        }
    }

    // http://www.policyalmanac.org/games/aStarTutorial.htm

    // F = G + H 
    // H : value for the DISTANCE a node is from the end (this includes weighted nodes)
    // G : The cost value it takes to get to this node (in the example diagonals cost slightly more).
    private void Awake()
    {
        nodesAcross = GridManager.Instance.NodesAcross;
        nodesUp     = GridManager.Instance.NodesUp;
    }

    /// <summary>
    /// Function to start the PathFinding process
    /// </summary>
    /// <param name="nodeID"></param>
    /// <param name="targetID"></param>
    /// <returns>Returns false if there has been an error regarding the targetID</returns>
    public bool Search(int nodeID, int targetID)
    {
        if(!InitSearch(nodeID, targetID))
        {
            return false;
        }


        Node currentNode = SelectNewParent();
        AddToClosedList(currentNode);
        CheckSurroundingNodes(currentNode);
        CheckForPath();

        return true;
    }


    public void ResetPath()
    {
       initSearch = false;
        openList.Clear();
        closedList.Clear();
        Path.Clear();
    }


    private bool InitSearch(int nodeID, int targetID)
    {
        Debug.Log("InitSearch bool is " + initSearch);
        if (!initSearch)
        {
            Node startNode = GridManager.Instance.GetNode(nodeID);
            targetNodeID = targetID;

            Debug.Log("Start node id: " + nodeID);
            Debug.Log("Target node id: " + targetID);

            if (startNode == null || GridManager.Instance.GetNode(targetNodeID) == null)
            {
                Debug.Log("Start or Target Node ID invalid");
                return false;
            }

            if (GridManager.Instance.GetNode(targetID).Weight == GridManager.SpriteWeight.Static)
            {
                Debug.Log("Target is covered");
                return false;
            }
            openList.Add(startNode);
            initSearch = true;
        }
        return true;
    }

    /// <summary>
    /// Main loop for the algorithm.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private IEnumerator SearchLoop(System.Action<bool> completed)
    {
        
        while (true)
        {
            //Debug.Log("LOOP BEGIN");
            //Debug.Log("Number of elements on the openList: " + openList.Count);

            Node currentNode = SelectNewParent();

            AddToClosedList(currentNode);
            CheckSurroundingNodes(currentNode);
            if(CheckForPath())
            {
                // return true;
                break;
            }

            completed(false);
            yield return null;
        }

        completed(true);
        yield return true;
    }

    /// <summary>
    /// For each of the adjascent nodes; check if it exists on the OpenList or not. 
    /// </summary>
    /// <param name="node"></param>
    private void CheckSurroundingNodes(Node node)
    {
        //Debug.Log("CHECKING SURROUNDING NODES");
        List<KeyValuePair<int, int>> adjascentNodes = GetAdjascentIDs(node.ID);
       // Debug.Log("Parent Node " + node.ID);

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

                //Debug.Log("Adding node " + adjascentNode.ID + " to openList");
               // Debug.Log("Adjascent node being checked " + adjascentNode.ID + 
                //    ", parentNode " + adjascentNode.Parent.ID      + 
                //    ", moveCost "  + adjascentNode.Cost            + 
                //    ", distance  " + adjascentNode.Distance        + 
                //    " and total "  + adjascentNode.TotalValue);
            }
            else
            {
               // Debug.Log("Node " + adjascentNode.ID + " is added to list.. Checking for better path");
                
                // Add the parent movecost to the potencial move cost if this direction was used. 
                int testCost = node.Cost + n.Value;
                int currentCost = node.Cost + adjascentNode.Cost;


                //Debug.Log("Parent node (" + node.ID + ") movement cost: " + node.Cost);
                //Debug.Log("New node movement cost: " + n.Value);
                //Debug.Log("Test move cost: " + testCost);


               // Debug.Log("Is " + testCost  + " < " + currentCost + "?");

                // if the test cost is less than the current adjascent movecost
                if(testCost < currentCost)
                {
                    // Change the parent and re-calc Cost and Dist. 
                    adjascentNode.Parent = node;
                    adjascentNode.Cost = testCost;
                    adjascentNode.TotalValue = adjascentNode.Distance + adjascentNode.Cost;


                   // Debug.Log("Updating Adjascent node to better path");
                   // Debug.Log("Adjascent node being checked " + adjascentNode.ID +
                   //     ", parentNode " + adjascentNode.Parent.ID +
                   //     ", moveCost " + adjascentNode.Cost +
                   //     ", distance  " + adjascentNode.Distance +
                   //     " and total " + adjascentNode.TotalValue);
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

        //bool upFlag = false;
        //bool downFlag = false;
        //bool leftFlag = false;
        //bool rightFlag = false;


        if (ID < (nodesAcross * nodesUp) - nodesAcross)
        {
            IDList.Add(new KeyValuePair<int,int>(ID + nodesAcross, orthogonalCost)); // UP
           // upFlag = true;
        }

        if (ID > nodesAcross)
        {
            IDList.Add(new KeyValuePair<int, int>(ID - nodesAcross, orthogonalCost)); // DOWN
            //downFlag = true;
        }

        Debug.Log("Nodes Across: " + nodesAcross);
        if (ID % nodesAcross > 0)
        {
            IDList.Add(new KeyValuePair<int, int>(ID - 1, orthogonalCost));  // left
           // leftFlag = true;
        }


        int y = (ID / nodesUp);
        if (ID != (y * nodesAcross) + (nodesAcross - 1))
        {
            IDList.Add(new KeyValuePair<int, int>(ID + 1, orthogonalCost)); // Right
            //rightFlag = true;
        }


        //if (upFlag)
        //{
        //    if (leftFlag)
        //    {
        //        IDList.Add(new KeyValuePair<int, int>((ID + nodesAcross) - 1, diagonalCost)); // UP_LEFT

        //    }
        //    if (rightFlag)
        //    {
        //        IDList.Add(new KeyValuePair<int, int>((ID + nodesAcross) + 1, diagonalCost)); // UP_RIGHT
        //    }
        //}

        //if (downFlag)
        //{
        //    if (leftFlag)
        //    {
        //        IDList.Add(new KeyValuePair<int, int>((ID - nodesAcross) - 1, diagonalCost)); // DOWN_LEFT

        //    }
        //    if (rightFlag)
        //    {
        //        IDList.Add(new KeyValuePair<int, int>((ID - nodesAcross) + 1, diagonalCost)); // DOWN_RIGHT
        //    }
        //}
        return IDList;
    }

    /// <summary>
    /// Sanity check the node being passed in and remove it from the openlist and 
    ///    add it to the closed list. 
    /// </summary>
    /// <param name="node"></param>
    private void AddToClosedList(Node node)
    {
       // Debug.Log("ADD TO THE CLOSED LIST");
        if (node.Equals(null))        return;
        if (!openList.Contains(node)) return;

        //Debug.Log("Add node " + node.ID + " to the closedList");
        

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
        //Debug.Log("SELECT A NEW PARENT");
        int minTotalValue = openList[0].TotalValue;
        Node parent = null;

        foreach (var node in openList)
        {
            if(node.TotalValue <= minTotalValue)
            {
                minTotalValue = node.TotalValue;
                parent = node;
            }
        }
       // Debug.Log("New Parent Node " + parent.ID);
        return parent;
    }


    /// <summary>
    /// Check for a path in the Closed list and check if a route cannot be found. 
    /// </summary>
    /// <returns></returns>
    private bool CheckForPath()
    {
        //Debug.Log("CHECK FOR PATH");
       // Debug.Log("Number of elements in the closed list : " + closedList.Count);
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

    /// <summary>
    /// Find the path starting at the targetNode. Add each element starting at the 
    ///     startNode to the PathList. 
    /// </summary>
    /// <param name="targetNode"></param>
    private void CreatePath(Node targetNode)
    {
        Node node = targetNode;
        Node[] pathBackwards = new Node[closedList.Count];

        // add the target node to the list.
        int idx = 0;
        while(true)
        {
            //Debug.Log("Adding node " + node.ID + " to the list at position " + idx);
            //pathBackwards[idx] = node;

            _path.Add(node);

            if (node.Parent == null)
                break;

            node = node.Parent;
            idx++;
        }

        _path.Reverse();
        //for(int i = idx; i >= 0; i--)
        //{
        //    _path.Add(pathBackwards[i]);
        //}

        for (int i = 0; i <= idx; i++)
        {
            Debugging.Instance.PlaceDebugCube(_path[i].Centre, _path[i].ID);
            Debug.Log("Path " + i + " is " + _path[i].ID);
        }
    }
}