using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weight = GridManager.SpriteWeight;

/// <summary>
/// Nodes are a part of the underlying invisible grid.
/// </summary>
public class Node
{
    public Node      Parent      { get; set; }
    public Vector3   Centre      { get; internal set; }
    public Weight    Weight      { get; internal set; }
    public int       ID          { get; internal set; }
    public int       TotalValue  { get; set; }
    public int       Distance    { get; set; }
    public int       Cost        { get; set; }


    public Node(Vector3 centre, int nodeID, Node parent)
    {
        ID     = nodeID;
        Centre = centre;
        Parent = parent;

        Cost = 0;
        Distance = 0;
        TotalValue = 0;

        CalculateWeight();
    }

    /// <summary>
    /// Calculate a weight for each node based on if any sprites are over that node. 
    /// </summary>
    private void CalculateWeight()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        float dir = cameraPos.z - Centre.z;
        Vector3 rayDirection = new Vector3(0, 0, dir);
        Debug.DrawRay(Centre, rayDirection, Color.yellow, 50);


        RaycastHit2D[] infoArray = Physics2D.RaycastAll(Centre, rayDirection, 5.0f);
        foreach(var hitInfo in infoArray)
        {
            Weight = GridManager.Instance.GetWeight(hitInfo.transform.tag);
        }
    }
}
