using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrigger : MonoBehaviour {

	public GameObject[] mazeTrigger = new GameObject[4];

	public Sprite[] mazes = new Sprite[4];

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
		Destroy (mazes [counter]);
		Destroy (mazeTrigger [counter]);
		counter++;	
		spawnMaze ();

	}

	void spawnMaze()
	{
		Instantiate (mazes [counter]);
	}
}
