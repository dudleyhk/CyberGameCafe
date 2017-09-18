using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonController : MonoBehaviour
{
	MainMenu menuHandler;

	void Start()
	{
		GetComponent<Button> ().onClick.AddListener (click);
		menuHandler = GameObject.FindGameObjectWithTag
			("GameController").GetComponent<MainMenu>();
	}

	void click()
	{
		switch (name) {
		case "New Game":
			menuHandler.newGame ();
			break;

		case "Continue":
			menuHandler.openGame ();
			break;

		case "Quit":
			menuHandler.closeGame ();
			break;

		default:
			Debug.Log ("Error with button");
			break;
		}
	}
}
