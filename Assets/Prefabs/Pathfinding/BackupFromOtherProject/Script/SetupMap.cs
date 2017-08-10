using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetupMap : MonoBehaviour
{
    public int[,] grid = null;
    
    private void Awake()
    {
        // if alread init return;

        grid = new int[MapData.cols, MapData.rows];

        // place a node and check if anything is there. 
        CreateGrid();

        var graph = new Graph(grid);
        var search = new Search(graph);
        search.Start(graph.nodes[0], graph.nodes[99]);

        while(!search.finished)
        {
            search.Step();
        }

        print("Search done. Path length " + search.path.Count + " iterations " + search.iterations);
	}

    /// <summary>
    /// This is just to findout the nodes type so it can be added to the int array.
    /// </summary>
    private void CreateGrid()
    {
        // for the total number of elements in the grid
        for (var r = 0; r < MapData.rows; r++)
        {
            for (var c = 0; c < MapData.cols; c++)
            {
                var centre = MapData.GetPositionOfIndex((MapData.cols * r) + c);
                var type = GetType(centre);
                grid[r, c] = type;

               //print("grid index " + (MapData.cols * r) + c + " is type " + type);
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
