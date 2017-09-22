using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour 
{
	public GameObject gameController;

	void OnTriggerEnter2D()
	{
		gameController.GetComponent<MazeTrigger> ().miniGameComplete ();
        Application.LoadLevel("SinglePlayer");
	}

}