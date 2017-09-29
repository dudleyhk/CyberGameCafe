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

    void Start()
    {
        //get a random message from the list
        setAllStrings();
        message = allMessages[Random.Range(0, allMessages.Count)];

        //tell the spawner which letters would be correct and spawn them
        GetComponent<Spawn>().setMessage(message.ToUpper());
        GetComponent<Spawn>().spawn();

        //put the message in the text box
        showMessage.GetComponent<Text>().text = message;
    }

    void setAllStrings()
    {
        allMessages.Add("Phishing");
        allMessages.Add("Wannacry");
        allMessages.Add("Security");
        allMessages.Add("USB Stick");
        allMessages.Add("Password");
        allMessages.Add("Computer");
        allMessages.Add("Networks");
        allMessages.Add("Academic");
    }
}
