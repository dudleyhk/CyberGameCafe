using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public GameObject debugSphere = null;
    public GameObject debugCube   = null;


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



    public void PlaceDebugSpheres(Vector2[] array, int i)
    {
        foreach (var position in array)
        {
            PlaceDebugSphere(position, i);
        }
    }
    public void PlaceDebugSphere(Vector3 position, int i)
    {
        GameObject clone = Instantiate(debugSphere);
        clone.name = "debugSphereClone_" + i.ToString();
        clone.transform.position = position;
    }
    public void PlaceDebugCube(Vector3 position, int i)
    {
        GameObject clone = Instantiate(debugCube);
        clone.name = "debugCubeClone_" + i.ToString();
        clone.transform.position = position;
    }
}
