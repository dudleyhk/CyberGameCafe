/*
 * this game object will persist through every scene, and this script will stay on it
 * this is how we determine how we did on a level that we have now switched away from
 * to find this script we can use:
 * 
 * if (GameObject.Find("EternalObject"))
 * {
 *      GameObject.Find("EternalObject").GetComponent<EternalScript>();
 * }
 * 
 * Give me a yell if you have trouble
 * - Thomas
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternalScript : MonoBehaviour
{

    public float encryptionScore;
    public float publicPCScore;
    public float USBScore;

    public int passwordScore;
    public int phishingScore;
    public int sharingScore;

    public char[] playerName;

    public void restart()
    {
        encryptionScore = -1f;
        publicPCScore = -1f;
        USBScore = -1f;

        passwordScore = -1;
        phishingScore = -1;
        sharingScore = -1;


        playerName = new char[8];
        for (int i = 0; i < 8; i++)
        {
            playerName[i] = ' ';
        }
    }

    void Update()
    {
        if (encryptionScore != -1f && publicPCScore != -1f && USBScore != -1f &&
            passwordScore != -1 && phishingScore != -1 && sharingScore != -1)
        {
            //game won show the score
        }
    }
}