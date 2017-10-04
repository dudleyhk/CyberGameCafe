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

    int numberOfLetters = 0;
    int currentLetter = 0;

    string message;
    
    public void setMessage(string s)
    {
        message = s;
    }

    public void spawn()
    {
        int lettersToSpawn = (numberOfLetters / 2) + 3;
        letterClone = new GameObject[lettersToSpawn];

        char correctLetter = advanceLetter(message[currentLetter]);

        //spawn the appropriate number of letter boxes
        for (int i = 0; i < lettersToSpawn; i++)
        {
            //instantiate them onto the canvas
            letterClone[i] = Instantiate(letterPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
            //put them somewhere random
            letterClone[i].transform.position = new Vector3
                (Random.Range(200, Screen.width - 200), Screen.height / 2, 0);

            //put in an incorrect letter
            char l;
            do
            {
                l = (char)('A' + Random.Range(0, 26));
            } while (l == correctLetter);
            letterClone[i].GetComponentInChildren<Text>().text = l.ToString();
        }

        //tag the correct letter and set it to what it needs to be
        letterClone[0].gameObject.tag = "CorrectLetter";
        letterClone[0].GetComponentInChildren<Text>().text = correctLetter.ToString();

        //move the player to their spawn
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(35, Screen.height / 2, 0);
    }

    public void newLetter()
    {
        //destroy the previous letters that were left
        for(int i = 0; i < letterClone.Length; i++)
        {
            if (letterClone[i])
            {
                Destroy(letterClone[i]);
            }
        }

        //get one extra letter for the next level
        numberOfLetters++;
        //move the correct letter on one
        currentLetter++;

        //check win state
        if (currentLetter == message.Length)
        {
            //get the gameobject that carries info between scenes and save the score onto it
            GameObject foreverController = GameObject.Find("EternalObject");
            if (foreverController)
            {
                float score = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().getTime();

                Debug.Log(foreverController.GetComponent<ConvertScore>().getRealScore(score, 20, 160, true));

                foreverController.GetComponent<EternalScript>().encryptionScore = score;
                Application.LoadLevel("SinglePlayer");
            }
            Debug.Log("You win");
        }
        else
        {
            //skip the spaces
            if (message[currentLetter] == ' ')
            {
                currentLetter++;
            }

            //spawn the new letters
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
