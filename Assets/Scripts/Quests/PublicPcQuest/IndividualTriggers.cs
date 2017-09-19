using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTriggers : MonoBehaviour {

	public GameObject gameController;
    private GameObject player;
    private GameObject mazeRunner;

	void OnTriggerEnter2D(Collider2D col)
	{
        player = GameObject.FindGameObjectWithTag("Player");
        mazeRunner = GameObject.FindGameObjectWithTag("MazeRunner");

        if (col.gameObject == player)
        {
            gameController.GetComponent<MazeTrigger>().MazeStart(player);
        }
        if (col.gameObject == mazeRunner)
        {
            gameController.GetComponent<MazeTrigger>().MazeComplete(player);
        }
	}
}
