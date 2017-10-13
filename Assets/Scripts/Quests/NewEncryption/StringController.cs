using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringController : MonoBehaviour
{
    string message;
    List<string> allMessages = new List<string>();

    [SerializeField]
    GameObject showMessage;

    void Awake()
    {
        //get a random message from the list
        setAllStrings();
        message = allMessages[Random.Range(0, allMessages.Count)];

        //tell the spawner which letters would be correct and spawn them
        GetComponent<Spawn>().setMessage(message.ToUpper());
        GetComponent<Spawn>().spawn();

        for(int i = 0; i < 8; i++)
        {
            //put the message into each text box
            showMessage.transform.GetChild(i).GetComponent<Text>().text = message[i].ToString();
        }
    }

    void setAllStrings()
    {
        allMessages.Add("Phishing");
        allMessages.Add("Wannacry");
        allMessages.Add("Security");
        allMessages.Add("USBStick");
        allMessages.Add("Password");
        allMessages.Add("Computer");
        allMessages.Add("Networks");
        allMessages.Add("Academic");
    }
}
