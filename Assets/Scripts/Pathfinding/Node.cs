using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Nodes are a part of the underlying invisible grid.
/// </summary>
public class Node
{
    public Vector3   Centre      { get; internal set; }
    public int       Weight      { get; internal set; }
    public int       ID          { get; internal set; }
    public Node      Parent      { get; set; }
	
	// Calculate these two values when the node is set up.
	public uint      DistanceToTarget {get;internal set;}
	public uint      Cost             {get; internal set;}


    public Node(Vector3 centre, int nodeID, Node parent, uint cost)
    {
        ID     = nodeID;
        Centre = centre;
        Parent = parent;
		Cost   = cost;

        CalculateWeight();
		CalculateDistance();
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

	
	private void CalculateDistance()
	{
		
		
	}
	
	
	private void CalculateCost()
	{
		// This can be calculated by getting where the parent is in relation to this node.
		// use parent.ID;
		
		
		
	}


}
