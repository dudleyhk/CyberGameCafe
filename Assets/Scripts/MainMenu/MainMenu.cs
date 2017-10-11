using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public void action(int menuItem)
    {
        switch(menuItem)
        {
            case 0:
                newGame();
                break;
            case 1:
                openGame();
                break;
            case 2:
                closeGame();
                break;
            default:
                break;
        }
    }

	void newGame()
	{
		clearGame ();
		openGame();
	}
		
	void openGame()
	{
		Application.LoadLevel ("SinglePlayer");
	}

	void closeGame()
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