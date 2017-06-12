using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Nodes are a part of the underlying invisible grid.
/// </summary>
public class Node
{
    public Node      ParentNode  { get; internal set; }
    public Vector3   Centre      { get; internal set; }
    public bool      Weight      { get; internal set; }


    public Node(Vector3 centre)
    {
        Centre = centre;
        CalculateWeight();
    }

    /// <summary>
    /// Calculate a weight for each node based on if any objects are over that tile. 
    /// </summary>
    private void CalculateWeight()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        float dir = cameraPos.z - Centre.z;
        Vector3 rayDirection = new Vector3(0, 0, dir);


        RaycastHit hitInfo;
        Ray ray = new Ray(Centre, rayDirection);
        int ignoreRaycast = 2;

        if(Physics.Raycast(ray, out hitInfo, ignoreRaycast))
        {
            // check if it hits any other sprites.

           // try adding a sprite into the scene and see if the raycast works
        }
    }
}
