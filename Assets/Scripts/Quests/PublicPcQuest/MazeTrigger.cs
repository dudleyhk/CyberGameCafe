using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrigger : MonoBehaviour {

	public GameObject[] mazeTrigger = new GameObject[4];

	public GameObject[] mazes = new GameObject[4];

	public GameObject blocker;

	private int counter = 0;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
		

	public void DestroyTrigger()
	{
		Destroy (mazeTrigger [counter]);
		counter++;	
		spawnMaze ();

	}

	void spawnMaze()
	{
		if (counter == 2) 
		{
			Destroy (mazes [(counter - 2)]);
		}
		blocker.transform.Translate (0, 0, -10);
	}
}
