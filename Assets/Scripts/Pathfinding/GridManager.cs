using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// cut the terrain into equal squares which are the same size as the character. 
public class GridManager : MonoBehaviour
{
    private List<Node> gridNodes = null;
    public GameObject map        = null;
    public GameObject character  = null;
    public float mapHeight    = 0.0f;
    public float mapWidth     = 0.0f;
    public float nodeHeight   = 0.0f;
    public float nodeWidth    = 0.0f;



    private void Awake()
    {
        // the height and width of the map.
        mapWidth   = map.transform.lossyScale.x;
        mapHeight  = map.transform.lossyScale.y;

        // the height and width of each square.
        nodeWidth  = character.transform.lossyScale.x;
        nodeHeight = character.transform.lossyScale.y;
    }

    private void Start()
    {
        CreateGrid();
    }


    private void CreateGrid()
    {
        // check to see if the grid has been made. 
        if (gridNodes.Count > 0)
            return;


        // to calculate initial weight of each square send a ray towards and away from the camera and see what it hits. 
        // Create my own ray cast class.

    }
}
