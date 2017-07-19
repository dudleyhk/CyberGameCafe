using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weight = GridManager.SpriteWeight;

/// <summary>
/// Nodes are a part of the underlying invisible grid.
/// </summary>
public class Node : MonoBehaviour
{
    public Node      Parent      { get; set; }
    public Vector3   Centre      { get; internal set; }
    public Weight    Weight      { get; internal set; }
    public int       ID          { get; internal set; }
    public int id_debugging = 0;



    /// <summary>
    /// The distance the node is from the target Node. 
    /// </summary>
    public int Distance { get; set; }

    /// <summary>
    /// Directional cost. Cost value set based on which direction is is from its parent. 
    /// </summary>
    public int Cost { get; set; }

    /// <summary>
    /// The Distance + Cost.
    /// </summary>
    public int TotalValue { get; set; }


    /// <summary>
    /// If Occupied another player cannot move into it.
    /// </summary>
    private bool _occupied = false;




    public void Init(Vector3 centre, int nodeID, Node parent)
    {
        ID     = nodeID;
        id_debugging = ID;
        Centre = centre;
        Parent = parent;

        Cost       = 0;
        Distance   = 0;
        TotalValue = 0;


        CalculateWeight();
    }



    /// <summary>
    /// Calculate a weight for each node based on if any sprites are over that node. 
    /// </summary>
    private void CalculateWeight()
    {
        Vector3 rayDirection = new Vector3(0, 0, -10f);
        Debug.DrawRay(Centre, rayDirection, Color.yellow, 50);


        RaycastHit2D[] infoArray = Physics2D.RaycastAll(Centre, rayDirection, 10.0f);
        foreach(var hitInfo in infoArray)
        {
            Weight = GridManager.Instance.GetWeight(hitInfo.transform.tag);
        }
    }



    public bool Occupied
    {
        get
        {
            return _occupied;
        }

        set
        {
            _occupied = value;
            if(_occupied)
            {
                //print("Node " + ID + " is occupied.");
            }
            else
            {
                //print("Node " + ID + " is now unoccupied.");
            } 
        }
    }
}
