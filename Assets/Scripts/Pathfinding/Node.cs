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
    public int       Weight      { get; internal set; }


    enum WeightType
    {
        None,
        Static
    }


    public Node(Vector3 centre)
    {
        Centre = centre;
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
            Weight = GetWeight(hitInfo.transform.tag);
        }
    }


    /// <summary>
    /// Return the enum value for the type of sprite which has been hit. 
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    private int GetWeight(string tag)
    {
        switch(tag)
        {
            case "StaticSprite":
                return System.Convert.ToInt32(WeightType.Static);
        }
        return System.Convert.ToInt32(WeightType.None);
        
    }
}
