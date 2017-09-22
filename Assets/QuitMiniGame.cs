using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMiniGame : MonoBehaviour {

    [SerializeField]
    string questTag;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(exit);
	}
	
    void exit()
    {
        Application.LoadLevel("SinglePlayer");
        //fail quest tag
    }
}
