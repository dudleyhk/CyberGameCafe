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
    public Vector2 mapPosition = Vector2.zero;
    public Vector2 mapMinPoint = Vector2.zero;
    public float mapHeight = 0.0f;
    public float mapWidth = 0.0f;

    public float nodeWidth = 0.0f;
    public float nodeHeight = 0.0f;
    public ushort nodesAcross = 0;
    public ushort nodesUp = 0;


    public Vector2[] vertexPositions = null;
    public GameObject debugObject = null;



    private void Awake()
    {
        // INIT MAP ATTRIBUTES
        mapRenderer = map.GetComponent<Renderer>();
        mapWidth = mapRenderer.bounds.size.x;
        mapHeight = mapRenderer.bounds.size.y;
        mapMinPoint = mapRenderer.bounds.min;
        mapPosition = map.transform.position;

        // INIT NODE ATTRIBUTES
        gridNodes = new List<Node>();
        nodeWidth = (float)mapWidth / nodesAcross;
        nodeHeight = (float)mapHeight / nodesUp;


        vertexPositions = new Vector2[(nodesAcross + 1) * (nodesUp + 1)];
    }


    private void Start()
    {
        InitVertexPositions();
        CreateNodes();
    }


    private void InitVertexPositions()
    {
        float xPos = mapMinPoint.x;
        float yPos = mapMinPoint.y;

        int i = 0;
        for (int y = 0; y <= nodesUp; y++)
        {
            xPos = mapMinPoint.x;
            for (int x = 0; x <= nodesAcross; x++)
            {
                vertexPositions[i] = new Vector2(xPos, yPos);
                xPos += nodeWidth;
                Debugging.Instance.PlaceDebugSphere(vertexPositions[i], i);
                i++;
            }
            yPos += nodeHeight;

        }
    }

    private void CreateNodes()
    {
        int newNodesUp = nodesUp + 1;
        int newNodesAcross = nodesAcross + 1;

        int i = 0;
        for (int y = 0; y <= nodesUp; y++)
        {
            for (int x = 0; x <= nodesAcross; x++)
            {
                if (i == (y * newNodesAcross) + (newNodesAcross - 1))
                {
                    // These are the right hand vertices which can be discounted
                    Debugging.Instance.PlaceDebugCube(vertexPositions[i], i);
                    i++;
                    continue;
                }
                if(i >= (newNodesUp * newNodesAcross) - newNodesAcross)
                {
                    // These are the top vertices which can be discounted
                    Debugging.Instance.PlaceDebugCube(vertexPositions[i], i);
                    i++;
                    continue;
                }

                gridNodes.Add(new Node(null));
                gridNodes[i].Coordinates[0] = vertexPositions[i];
                gridNodes[i].Coordinates[1] = vertexPositions[i + 1];
                gridNodes[i].Coordinates[2] = vertexPositions[i + nodesAcross + 1];
                gridNodes[i].Coordinates[3] = vertexPositions[i + nodesUp + 2];
                i++;
            }
        }
    }
} 