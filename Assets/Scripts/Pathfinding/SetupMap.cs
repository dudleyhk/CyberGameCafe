using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMap : MonoBehaviour
{
    public static int[,] grid = null;
    public static NodeGraph nodeGraph = null;
    public GameObject nodeSprite;
    public GameObject characterPrefab; // Debugging
    
    private void Start()
    {
        // if alread init return;

        
        
        // CREATING GRID.
        grid = new int[MapData.cols, MapData.rows];
        GridElementTypes();

        nodeGraph = new NodeGraph(grid);
    }

    /// <summary>
    /// This is just to findout the nodes type so it can be added to the int array.
    /// </summary>
    private void GridElementTypes()
    {
        // for the total number of elements in the grid
        for (var r = 0; r < MapData.rows; r++)
        {
            for (var c = 0; c < MapData.cols; c++)
            {
                var centre = MapData.GetPositionOfIndex((MapData.cols * r) + c);
                var type = GetType(centre);
                grid[r, c] = type;

               // if(type == 0)
                 //   print("grid index " + (MapData.cols * r) + c + " is type " + type);
            }
        }
    }

    /// <summary>
    /// Check if the raycast has hit an obsticle. 
    /// </summary>
    /// <param name="centre"></param>
    /// <returns></returns>
    private int GetType(Vector3 centre)
    {
        int type = 0;
        Vector3 rayDirection = new Vector3(0, 0, -10f);
        Debug.DrawRay(centre, rayDirection, Color.yellow, 50);

        RaycastHit2D[] infoArray = Physics2D.RaycastAll(centre, rayDirection, 10.0f);
        foreach (var hitInfo in infoArray)
        {
            if (hitInfo.transform.tag == "Obsticle")
            {
                type = 1;
            }
        }
        return type;
    }


    //private Image GetImage(string label)
    //{
    //    var id = Int32.Parse(label);
    //    var go = mapGroup.transform.GetChild(id).gameObject;
    //    return go.GetComponent<Image>();
    //}



    //private void ResetMapGroup(Graph graph)
    //{
    //    foreach (var node in graph.nodes)
    //    {
    //        GetImage(node.label).color = node.adjecent.Count == 0 ? Color.white : Color.grey;
    //    }
    //}

}
