using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ReadHighScoreFile : MonoBehaviour
{ 
    StreamReader fileReader;

    // Use this for initialization
    void Start ()
    {
		fileReader = fileReader = new StreamReader("assets\\scripts\\mainmenu\\topScores.highScore");
        for(int i = 0; i < 3; i++)
        {
            string playerName = fileReader.ReadLine();
            string playerScore = fileReader.ReadLine();
            Text t = GetComponent<Text>();
            t.text = t.text + "\n" + playerName + "\t" + playerScore;
        }
        fileReader.Close();
    }
}
