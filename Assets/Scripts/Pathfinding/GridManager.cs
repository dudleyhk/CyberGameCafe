using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// to calculate initial weight of each square send a ray towards and away from the camera and see what it hits. 
// Create my own ray cast class.
// cut the terrain into equal squares which are the same size as the character. 
public class GridManager : MonoBehaviour
{
    private List<Node> gridNodes = null;
    private Renderer mapRenderer = null;
    public GameObject map = null;
    public Vector3 mapMinPoint = Vector3.zero;
    public float mapHeight = 0.0f;
    public float mapWidth = 0.0f;

    public float nodeWidth = 0.0f;
    public float nodeHeight = 0.0f;
    public ushort nodesAcross = 0;
    public ushort nodesUp = 0;
    public ushort nodeDepth = 5;



    private void Awake()
    {
        // INIT MAP ATTRIBUTES
        mapRenderer = map.GetComponent<Renderer>();
        mapWidth = mapRenderer.bounds.size.x;
        mapHeight = mapRenderer.bounds.size.y;
        mapMinPoint = mapRenderer.bounds.min;

        // INIT NODE ATTRIBUTES
        gridNodes = new List<Node>();
        nodeWidth = (float)mapWidth / nodesAcross;
        nodeHeight = (float)mapHeight / nodesUp;
    }


    private void Start()
    {
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
                float TRx = currentX + nodeWidth;
                float TRy = currentY + nodeHeight;
                Vector2 topRight = new Vector2(TRx, TRy);

                float Cx = topRight.x - (nodeWidth / 2);
                float Cy = topRight.y - (nodeHeight / 2);
                float Cz = mapMinPoint.z + nodeDepth;
                Vector3 centre = new Vector3(Cx, Cy, Cz);

                Debugging.Instance.PlaceDebugSphere(centre, (int)(i * nodesAcross + j));

                gridNodes.Add(new Node(centre));

                currentX += nodeWidth;
            }
            currentY += nodeHeight;
        }
    }
} 