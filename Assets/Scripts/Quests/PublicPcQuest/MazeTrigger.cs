using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrigger : MonoBehaviour {

	public GameObject[] mazeTrigger = new GameObject[4];

	public GameObject[] mazeCompleteTriggers = new GameObject[4];

	public GameObject[] mazes = new GameObject[4];

	public GameObject blocker;

	public GameObject gameEndTrigger;

    public GameObject mazeRunner;
	private GameObject mazeRunnerClone;
	private GameObject currentObjective;

	private int counter = 0;

    private GameObject player;

	public void MazeStart(GameObject thePlayer)
	{
        player = thePlayer;
        player.GetComponent<Movement>().stopMovement();

		Destroy (mazeTrigger [counter]);
		counter++;	
		spawnMaze ();

		mazeRunnerClone = Instantiate(mazeRunner);
		mazeRunnerClone.transform.position = new Vector3(0, 0, 0);
		mazeRunnerClone.transform.position = new Vector3(39.6f, 36.6f, -2f);
       
    }

    public void MazeComplete(GameObject thePlayer)
    {
        player = thePlayer;
        player.GetComponent<Movement>().startMovement();

		Destroy(mazeRunnerClone);
		Destroy (currentObjective);
    }

	public void miniGameComplete()
	{
		Application.LoadLevel ("SinglePlayer");
	}



	void spawnMaze()
	{
		if (counter > 1) 
		{
			Destroy (mazes [(counter - 2)]);
		}
		blocker.transform.Translate (0, 0, -10);

		currentObjective = mazeCompleteTriggers [counter-1];
		BoxCollider2D currentObjectiveBoxCollider = currentObjective.GetComponent<BoxCollider2D> ();
		currentObjectiveBoxCollider.enabled = true;
		Debug.Log (counter + " is enabled");
	}
}
