﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMap : MonoBehaviour
{
    public bool[,] grid = null;
    public static NodeGraph nodeGraph = null;
    public GameObject nodeSprite;


    
    private void Start()
    {
        // if alread init return;

        
        
        // CREATING GRID.
        grid = new bool[MapData.rows, MapData.cols];
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
                var solid = GetType(centre);
                
                grid[r, c] = solid;
                
                //if (solid == true)
                //    print("grid index " + ((MapData.cols * r) + c) + " is solid: " + solid);
            }
        }
    }

    /// <summary>
    /// Check if the raycast has hit an obsticle. 
    /// </summary>
    /// <param name="centre"></param>
    /// <returns></returns>
    private bool GetType(Vector3 centre)
    {
        bool solid = false;
        Vector3 rayDirection = new Vector3(0, 0, -10f);
        Debug.DrawRay(centre, rayDirection, Color.yellow, 50);

        RaycastHit2D[] infoArray = Physics2D.RaycastAll(centre, rayDirection, 10.0f);
        foreach (var hitInfo in infoArray)
        {
            if (hitInfo.transform.tag == "Obsticle")
            {
                solid = true;
            }
        }
        return solid;
    }
}
