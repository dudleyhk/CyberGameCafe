using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    string[] content = new string[5];

    [SerializeField]
    GameObject eMailBox;
    private Button button;

    List<char> charQueue = new List<char>();
    float delay;

    bool[] flags = new bool[12];
    bool[] caught = new bool[12];

    private int score;
    private int maxPossScore;

    public int getScore()
    {
        return score;
    }
    public int getMaxScore()
    {
        return maxPossScore;
    }
    public void resetScore()
    {
        score = 0;
        maxPossScore = 0;
    }

    void Awake()
    {
        resetScore();

        delay = -10f;

        button = eMailBox.GetComponentInChildren<Button>();
        button.onClick.AddListener(checkForFlag);

        StreamReader emailReader;
        emailReader = new StreamReader("Assets\\Scripts\\Quests\\SpamQuest\\emails.txt");
        
        for (int i = 0; i < content.Length; i++)
        {
            content[i] = emailReader.ReadLine();
        }
        emailReader.Close();

        for(int i = 0; i < 12; i++)
        {
            flags[i] = false;
            caught[i] = false;
        }
    }

    //when a button is pressed by the player
    //check if there was an error on screen and if so count the player as having gotten it
    public void checkForFlag()
    {
        for(int i = 0; i < 12; i++)
        {
            if(flags[i])
            {
                caught[i] = true;
            }
        }
    }

    void FixedUpdate()
    {
        Text textBox = eMailBox.GetComponentInChildren<Text>();
        if (charQueue.Count > 0 && delay > 0.1f)
        {
            if (charQueue[0] != '#' && charQueue[0] != '~')
            {
                //print the first availible character
                textBox.text = textBox.text + charQueue[0];
            }
            else
            {
                int x = (int)charQueue[1] - 48;
                flags[x] = (charQueue[0] == '#');

                //if the flag ends check whether the player caught it
                if (!flags[x])
                {
                    if (!caught[x])
                    {
                        //add this to the list of pointers the NPC will give the player before they try again
                        GetComponent<ThingsToRemember>().addMissedAnswer(x);
                    }
                    else
                    {
                        score++;
                    }
                    maxPossScore++;
                }

                charQueue.RemoveAt(0);
            }
            charQueue.RemoveAt(0);
            
            delay = 0f;
        }
        if(charQueue.Count == 0 && delay > 3)
        {
            eMailBox.SetActive(false);
        }
        delay += Time.deltaTime;
    }

    public void printEmail(int index)
    {
        Text textBox = eMailBox.GetComponentInChildren<Text>();
        textBox.text = "";
        eMailBox.SetActive(true);
        
        string message = content[index];
        for(int i = 0; i < message.Length; i++)
        {
            if(message[i] == '\\')
            {
                switch (message[i + 1])
                {
                    case 't':
                        charQueue.Add('\t');
                        break;
                    case 'n':
                        charQueue.Add('\n');
                        break;
                }
                i++;
            }

            else
            {
                charQueue.Add(message[i]);
            }
        }
    }

}
