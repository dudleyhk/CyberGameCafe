using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrigger : MonoBehaviour {

	public GameObject[] mazeTrigger = new GameObject[4];

	public GameObject[] mazes = new GameObject[4];

	public GameObject blocker;

	private int counter = 0;

    private GameObject player;

         // Use this for initialization
        void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
		

	public void MazeStart(GameObject thePlayer)
	{
        player = thePlayer;
        player.GetComponent<Movement>().stopMovement();

		Destroy (mazeTrigger [counter]);
		counter++;	
		spawnMaze ();

	}

    public void MazeComplete(GameObject thePlayer)
    {
        player = thePlayer;
        player.GetComponent<Movement>().startMovement();
    }

	void spawnMaze()
	{
		if (counter > 1) 
		{
			Destroy (mazes [(counter - 2)]);
		}
		blocker.transform.Translate (0, 0, -10);
	}
}
