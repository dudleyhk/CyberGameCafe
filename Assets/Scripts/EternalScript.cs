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
using UnityEngine.SceneManagement;

public class EternalScript : MonoBehaviour
{
    public bool onWinScreen;
    bool quitArm;

    public float encryptionScore;
    public float publicPCScore;
    public float USBScore;

    public int passwords;
    public int passwordScore;
    public int phishingScore;
    public int phishingMax;
    public int sharingScore;

    public char[] playerName;

    void Start()
    {
        quitArm = false;

        onWinScreen = false;
        encryptionScore = -1f;
        publicPCScore = -1f;
        USBScore = -1f;

        passwords = 0;
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
            passwords == 3 && phishingScore != -1 && sharingScore != -1 &&
            !onWinScreen)
        {
            onWinScreen = true;
            SceneManager.LoadScene("WinScene");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            quitArm = !quitArm;
        }

        if (quitArm && Input.GetKeyDown(KeyCode.Y))
        {
            Destroy(GameObject.Find("Audio(Clone)"));
            Destroy(GameObject.Find("Audio"));
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
        }
    }
}