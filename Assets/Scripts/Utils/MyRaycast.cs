using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MyRaycast
{
    // My raycast has customisable params.
    // My raycast has a debug line. 


    public Vector3 start     = Vector3.zero;
    public Vector3 direction = Vector3.zero;


    struct HitData
    {
        public Vector3 intersectPoint;
    }


    public MyRaycast(Vector3 start, Vector3 direction)
    {
        this.start = start;
        this.direction = direction;

        Debug.DrawRay(start, direction, Color.yellow, 50);
        
    }


    public bool IntersectionCheck()
    {
        
        return false;
    }
}
