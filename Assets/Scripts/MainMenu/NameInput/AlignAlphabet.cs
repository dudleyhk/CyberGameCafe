using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignAlphabet : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GameObject alphabetty = GameObject.Find("Alphabetty");
        for (int i = 0; i < 29; i++)
        {
            alphabetty.transform.GetChild(i).transform.position =
                alphabetty.transform.position + 
                new Vector3((i % 6) * 50, (4 - (i / 6)) * 50, 0);
        }
    }
}
