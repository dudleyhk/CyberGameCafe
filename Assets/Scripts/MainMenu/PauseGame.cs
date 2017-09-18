using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour 
{	
	[SerializeField]
	GameObject pauseMenu;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.P)) 
		{
			GameObject.FindGameObjectWithTag ("Player").
			GetComponent<Movement>().stopMovement();
			pauseMenu.SetActive (true);
		}
	}
}
