using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScores : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject scorer = GameObject.Find("EternalObject");
        if (scorer)
        {
            EternalScript s = scorer.GetComponent<EternalScript>();
            int[] score = new int[6];

            GetComponent<Text>().text =
                "Phising Spam Score: " + s.phishingScore +
                "\nSecure Password Score: " + s.passwordScore +
                "\nEncryption Score: " + s.encryptionScore +
                "\nPassword Sharing Score: " + s.sharingScore +
                "\nPublic PCs Score: " + s.publicPCScore +
                "\nUSB Score: " + s.USBScore;
        }
    }
}
