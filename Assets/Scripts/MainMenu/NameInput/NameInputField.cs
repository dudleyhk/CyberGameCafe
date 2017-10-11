using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour {

    EternalScript e;

    void Start()
    {
        GameObject g;
        if (g = GameObject.Find("EternalObject"))
        {
            e = g.GetComponent<EternalScript>();
        }
    }
    
	void Update ()
    {
		if(e)
        {
            GetComponent<Text>().text = "";
            for (int i = 0; i < 8; i++)
            {
                GetComponent<Text>().text += e.playerName[i];
            }
        }
	}
}
