using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectLetter : MonoBehaviour {

    int index;
    Text t;
    GameObject objectParent;
    
	void Start ()
    {
        index = 0;
        t = GetComponent<Text>();
        getDaddy();
	}
	
	void getDaddy ()
    {
        objectParent = transform.parent.gameObject;
        t.text = ((char)(objectParent.GetComponent<Text>().text[0] + 1)).ToString();
    }

    public void advance()
    {
        index++;
        transform.SetParent(objectParent.transform.parent.GetChild(index), false);
        getDaddy();
    }
}
