using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// to calculate initial weight of each square send a ray towards and away from the camera and see what it hits. 
// Create my own ray cast class.
// cut the terrain into equal squares which are the same size as the character. 
public class GridManager : MonoBehaviour
{
    private Renderer mapRenderer = null;
    public List<Node> gridNodes = new List<Node>();
    public GameObject nodePrefab = null;
    public GameObject map = null;
    public Vector3 mapMinPoint = Vector3.zero;
    public Vector3 mapMaxPoint = Vector3.zero;
    public float mapHeight = 0.0f;
    public float mapWidth = 0.0f;
    public ushort spawnNodeID = 0;

    public float  _nodeWidth = 0.0f;
    public float  _nodeHeight = 0.0f;
    public ushort _nodesAcross = 0;
    public ushort _nodesUp = 0;
    public ushort _totalNodes = 0;
    public ushort nodeDepth_debugging = 5;
    public bool gridCubes_debugging = false;



    public enum SpriteWeight
    {
        None = 0,
        Static = 1
    }



    public ushort NodesUp
    {
        get
        {
            return _nodesUp;
        }
    }
    public ushort NodesAcross
    {
        get
        {
            return _nodesAcross;
        }
    }
    public float NodeWidth
    {
        get
        {
            return _nodeWidth;
        }

        set
        {
            _nodeWidth = value;
        }
    }
    public float NodeHeight
    {
        get
        {
            return _nodeHeight;
        }

        set
        {
            _nodeHeight = value;
        }
    }
    public ushort TotalNodes
    {
        get
        {
            return _totalNodes;
        }

        set
        {
            _totalNodes = value;
        }
    }
    public Node SpawnNode
    {
        get
        {
            return GetNode(spawnNodeID);
        }
    }


    private static GridManager _instance = null;
    public static GridManager Instance
    {
        get
        {
            if (!_instance)
            {
                var gridManager = GameObject.FindObjectOfType<GridManager>();
                _instance = gridManager;
            }
            return _instance;
        }        
    }



    private void Awake()
    {
        // INIT MAP ATTRIBUTES
        mapRenderer = map.GetComponent<Renderer>();
        mapWidth = mapRenderer.bounds.size.x;
        mapHeight = mapRenderer.bounds.size.y;
        mapMinPoint = mapRenderer.bounds.min;
        mapMaxPoint = mapRenderer.bounds.max;

        // INIT NODE ATTRIBUTES
        NodeWidth = (float)mapWidth / NodesAcross;
        NodeHeight = (float)mapHeight / NodesUp;

        int total = (int)NodesUp * NodesAcross;
        TotalNodes = (ushort)total;

        BuildGrid();
    }


    /// <summary>
    /// Break the map into equal nodes based on the number of nodes specified in the Editor, 
    /// and create new Nodes using the nodes centre position. 
    /// </summary>
    private void BuildGrid()
    {
        float currentX = mapMinPoint.x;
        float currentY = mapMinPoint.y;

        for (uint i = 0; i < NodesUp; i++)
        {
            currentX = mapMinPoint.x;
            for(uint j = 0; j < NodesAcross; j++)
            {
                int nodeIdx = (int)(i * NodesAcross + j);

                float TRx = currentX + NodeWidth;
                float TRy = currentY + NodeHeight;
                Vector2 topRight = new Vector2(TRx, TRy);

                float Cx = topRight.x - (NodeWidth / 2);
                float Cy = topRight.y - (NodeHeight / 2);
                float Cz = mapMinPoint.z;//; + nodeDepth_debugging;
                Vector3 centre = new Vector3(Cx, Cy, Cz);

                if(gridCubes_debugging)
                    Debugging.Instance.PlaceDebugSphere(centre, nodeIdx);

                // Create a new node and initialise it. 
                Node node = Instantiate(nodePrefab, this.transform).GetComponent<Node>();
                node.Init(centre, nodeIdx, null);
                node.transform.position = centre;

                // Set scale of the node object.
                Vector3 nodeScale = node.transform.localScale;
                nodeScale.x = NodeWidth;
                nodeScale.y = NodeHeight;
                node.transform.localScale = nodeScale;


                // Add it to the list.
                gridNodes.Add(node);

                // Move to next node along. 
                currentX += NodeWidth;
            }
            currentY += NodeHeight;
        }
    }


    /// <summary>
    /// Return the enum value for the type of sprite which has been hit. 
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public SpriteWeight GetWeight(string tag)
    {
        switch (tag)
        {
            case "StaticSprite":
                return SpriteWeight.Static;
        }
        return SpriteWeight.None;
    }

    /// <summary>
    /// If the list isn't empty and if the gridNode at ID is valid return the specified gridNode,
    ///     other wise return null.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Node GetNode(int ID)
    {
        if (gridNodes.Count <= 0)
            return null;

        if (ID > (NodesAcross * NodesUp) || ID < 0)
            return null;

        return gridNodes[ID];
    }


    //public Node GetNodeAtPosition(Vector3 position)
    //{
    //    float posX = position.x;
    //    float posY = position.y;

    //    // If the position is outside of the map space. 
    //    if ((position.x < mapMinPoint.x) && (position.x > mapMaxPoint.x))
    //    {
    //        if ((position.y < mapMinPoint.y) && (position.y > mapMaxPoint.y))
    //        {
    //            print("Character is outside the map area");
    //        }
    //    }

    //}


}