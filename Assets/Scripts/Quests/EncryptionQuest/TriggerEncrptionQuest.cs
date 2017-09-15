using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEncrptionQuest : MonoBehaviour
{
    private GameObject player;
    private bool playerInBox;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInBox == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Scene Switch to Encryption");
            Application.LoadLevel(1);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            Debug.Log("playing in encrpytion trigger");
            playerInBox = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            playerInBox = false;
        }
    }
}
