using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour 
{	
	[SerializeField]
	GameObject pauseMenu;

    public GameObject currentObjectiveUI;

	// Update is called once per frame
    void Start()
    {
        // currentObjectiveUI = GameObject.FindGameObjectWithTag("CurrentObjective");
    }

	void Update ()
	{
		if (Input.GetButtonDown("Pause")) 
		{
			GameObject.FindGameObjectWithTag ("Player").
			GetComponent<Movement>().stopMovement();

            if (currentObjectiveUI.activeInHierarchy) { currentObjectiveUI.SetActive(false); }

            pauseMenu.SetActive (true);


		}
	}
}
