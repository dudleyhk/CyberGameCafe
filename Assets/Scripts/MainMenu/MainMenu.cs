using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
	public void newGame()
	{
		clearGame ();
		openGame();
	}
		
	public void openGame()
	{
		Application.LoadLevel ("SinglePlayer");
	}

	public void closeGame()
	{
		Application.Quit ();
	}


	void clearGame()
	{
		StreamWriter fileWriter = new StreamWriter ("assets\\scripts\\quests\\questprogress\\questprogress.txt");

		fileWriter.WriteLine("");

		fileWriter.Close ();
	}
}