//please ensure messages only use spaces and letters
//please ensure messages do not use two consecutive spaces

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    GameObject letterPrefab;
    GameObject[] letterClone;

    int numberOfLetters = 3;
    int currentLetter = 0;

    string message;
    
    public void setMessage(string s)
    {
        message = s;
    }

    public void spawn()
    {
        letterClone = new GameObject[numberOfLetters];

        char correctLetter = advanceLetter(message[currentLetter]);

        //spawn the appropriate number of letter boxes
        for (int i = 0; i < numberOfLetters; i++)
        {
            //instantiate them onto the canvas
            letterClone[i] = Instantiate(letterPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
            //put them somewhere random
            letterClone[i].transform.position = new Vector3
                (Random.Range(50, Screen.width - 50), Random.Range(50, Screen.height - 50), 0);

            //put in an incorrect letter
            char l;
            do
            {
                l = (char)('A' + Random.Range(0, 26));
            } while (l == correctLetter);
            letterClone[i].GetComponentInChildren<Text>().text = l.ToString();
        }

        letterClone[0].gameObject.tag = "CorrectLetter";
        letterClone[0].GetComponentInChildren<Text>().text = correctLetter.ToString();
    }

    public void newLetter()
    {
        for(int i = 0; i < letterClone.Length; i++)
        {
            Destroy(letterClone[i]);
        }
        numberOfLetters++;
        currentLetter++;

        if (currentLetter == message.Length)
        {
            Debug.Log("You win");
        }
        else
        {
            if (message[currentLetter] == ' ')
            {
                currentLetter++;
            }
            spawn();
        }
    }

    char advanceLetter(char c)
    {
        if (c == 'Z')
        {
            return 'A';
        }
        else
        {
            return (char)((int)c + 1);
        }
    }
}
