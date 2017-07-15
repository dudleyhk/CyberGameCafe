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
    public float mapHeight = 0.0f;
    public float mapWidth = 0.0f;

    public float nodeWidth = 0.0f;
    public float nodeHeight = 0.0f;
    public ushort nodesAcross = 0;
    public ushort nodesUp = 0;
    public ushort nodeDepth = 5;



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


    public enum SpriteWeight
    {
        None = 0,
        Static = 1
    }



    /// <summary>
    /// Return the enum value for the type of sprite which has been hit. 
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    /// 
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

        if (ID > (nodesAcross * nodesUp) || ID < 0)
            return null;


        return gridNodes[ID];
    }


    public ushort GetNodesUp()
    {
        return nodesUp;
    }


    public ushort GetNodesAcross()
    {
        return nodesAcross;
    }


    private void Awake()
    {
        // INIT MAP ATTRIBUTES
        mapRenderer = map.GetComponent<Renderer>();
        mapWidth = mapRenderer.bounds.size.x;
        mapHeight = mapRenderer.bounds.size.y;
        mapMinPoint = mapRenderer.bounds.min;

        // INIT NODE ATTRIBUTES
        nodeWidth = (float)mapWidth / nodesAcross;
        nodeHeight = (float)mapHeight / nodesUp;

        CreateNodes();
    }


    /// <summary>
    /// Break the map into equal nodes based on the number of nodes specified in the Editor, 
    /// and create new Nodes using the nodes centre position. 
    /// </summary>
    private void CreateNodes()
    {
        float currentX = mapMinPoint.x;
        float currentY = mapMinPoint.y;

        for (uint i = 0; i < nodesUp; i++)
        {
            currentX = mapMinPoint.x;
            for(uint j = 0; j < nodesAcross; j++)
            {
                int nodeIdx = (int)(i * nodesAcross + j);

                float TRx = currentX + nodeWidth;
                float TRy = currentY + nodeHeight;
                Vector2 topRight = new Vector2(TRx, TRy);

                float Cx = topRight.x - (nodeWidth / 2);
                float Cy = topRight.y - (nodeHeight / 2);
                float Cz = mapMinPoint.z + nodeDepth;
                Vector3 centre = new Vector3(Cx, Cy, Cz);

               // Debugging.Instance.PlaceDebugSphere(centre, nodeIdx);

                // Create a new node and initialise it. 
                Node node = Instantiate(nodePrefab, this.transform).GetComponent<Node>();
                node.Init(centre, nodeIdx, null);
                node.transform.position = centre;

                // Set scale of the node object.
                Vector3 nodeScale = node.transform.localScale;
                nodeScale.x = nodeWidth;
                nodeScale.y = nodeHeight;
                node.transform.localScale = nodeScale;


                // Add it to the list.
                gridNodes.Add(node);

                // Move to next node along. 
                currentX += nodeWidth;
            }
            currentY += nodeHeight;
        }
    }
} 