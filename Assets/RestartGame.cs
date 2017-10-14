using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RestartGame : MonoBehaviour
{
    StreamReader fileReader;
    StreamWriter fileWriter;
    List<string> names;
    List<int> scores;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            saveScore();
            Destroy(GameObject.Find("EternalObject"));
            Destroy(GameObject.Find("Audio"));
            Destroy(GameObject.Find("Audio(Clone)"));
            SceneManager.LoadScene("MainMenu");
        }
    }

    void saveScore()
    {
        //get the scores from the text file into a big ol list
        names = new List<string>();
        scores = new List<int>();
        fileReader = new StreamReader("assets\\scripts\\mainmenu\\topScores.highScore");

        string lineName;
        string score;

        do
        {
            lineName = fileReader.ReadLine();
            score = fileReader.ReadLine();
            names.Add(lineName);
            scores.Add(int.Parse(score));
        } while (!fileReader.EndOfStream);
        fileReader.Close();
        
        int playerScore = GameObject.Find("Score").GetComponent<ShowScores>().getScore();

        for (int i = 0; i < names.Count; i++)
        {
            if (playerScore > scores[i])
            {
                string n = "";
                char[] arrayOfChars = GameObject.Find("EternalObject").GetComponent<EternalScript>().playerName;
                for (int j = 0; j < arrayOfChars.Length; j++)
                {
                    n += arrayOfChars[j];
                }
                names.Insert(i, n);
                scores.Insert(i, playerScore);
                break;
            }
        }

            fileWriter = new StreamWriter("assets\\scripts\\mainmenu\\topScores.highScore");
        
        for (int i = 0; i < names.Count; i++)
        {
            fileWriter.WriteLine(names[i]);
            fileWriter.WriteLine(scores[i]);
        }
        fileWriter.Close();
    }
}