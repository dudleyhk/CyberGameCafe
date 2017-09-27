using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour 
{	
	[SerializeField]
	GameObject pauseMenu;

    GameObject currentObjectiveUI;

	// Update is called once per frame
    void Start()
    {
        currentObjectiveUI = GameObject.FindGameObjectWithTag("CurrentObjective");
    }

	void Update ()
	{
		if (Input.GetKey (KeyCode.P)) 
		{
			GameObject.FindGameObjectWithTag ("Player").
			GetComponent<Movement>().stopMovement();

            if (currentObjectiveUI.activeInHierarchy) { currentObjectiveUI.SetActive(false); }

            pauseMenu.SetActive (true);

		}
	}
}
