using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public GameObject debugSphere = null;


    private static Debugging instance = null;
    public static Debugging Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<Debugging>();
            }
            return instance;
        }
    }



    public void PlaceDebugSpheres(Vector2[] array)
    {
        foreach (var position in array)
        {
            PlaceDebugSphere(position);
        }
    }
    public void PlaceDebugSphere(Vector3 position)
    {
        GameObject debugSphereClone = Instantiate(debugSphere);
        debugSphereClone.transform.position = position;
    }
}
