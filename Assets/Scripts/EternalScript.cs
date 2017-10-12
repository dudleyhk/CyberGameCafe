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

public class EternalScript : MonoBehaviour {

    public float encryptionScore;
    public float publicPCScore;
    public int sharingScore;
    public char[] playerName;

	void Awake()
    {
        playerName = new char[8];
        for (int i = 0; i < 8; i++)
        {
            playerName[i] = ' ';
        }
        DontDestroyOnLoad(gameObject);
    }
}
