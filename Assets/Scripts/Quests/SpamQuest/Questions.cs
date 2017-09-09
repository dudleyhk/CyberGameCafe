using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    string[] content = new string[6];

    [SerializeField]
    GameObject eMailBox;

    List<char> charQueue = new List<char>();
    float delay = 0f;

    void Awake()
    {
        StreamReader emailReader;
        emailReader = new StreamReader("Assets\\Scripts\\Quests\\SpamQuest\\emails.txt");
        
        for (int i = 0; i < content.Length; i++)
        {
            content[i] = emailReader.ReadLine();
        }
        emailReader.Close();
    }
    
    void Update()
    {
        Text textBox = eMailBox.GetComponentInChildren<Text>();
        if (charQueue.Count > 0 && delay > 0.08f)
        {
            //print the first availible character
            textBox.text = textBox.text + charQueue[0];
            charQueue.RemoveAt(0);
            
            delay = 0f;
        }
        delay += Time.deltaTime;
    }

    public void printEmail(int index)
    {
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

            else if(message[i] == '#' || message[i] == '~')
            {
                //do a fix of this
                i++;
            }

            else
            {
                charQueue.Add(message[i]);
            }
        }
    }

}
