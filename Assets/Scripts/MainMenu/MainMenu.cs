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
                closeGame();
                break;
            default:
                break;
        }
    }

	void newGame()
	{
        GameObject.Find("EternalObject").GetComponent<EternalScript>().restart();
		Application.LoadLevel ("NameInput");
	}

	void closeGame()
	{
		Application.Quit ();
	}
}