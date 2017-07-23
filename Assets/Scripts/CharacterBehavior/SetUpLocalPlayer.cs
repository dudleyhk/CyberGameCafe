//ensures player input only affects their sprite
//Can send pScore between server and client, which is useful

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour
{ 
    int[] uniqueNumber = new int[40];

    [SyncVar]
    public int hair;
    [SyncVar]
    public int top;
    [SyncVar]
    public int legs;
    [SyncVar]
    public int eyes;
    [SyncVar]
    public int body;

    [SyncVar]
    public Color skinC;
    [SyncVar]
    public Color hairC;
    [SyncVar]
    public Color topC;
    [SyncVar]
    public Color shoesC;
    [SyncVar]
    public Color legsC;

    private Camera mainCam;
        
    void Start ()
    {
        uniqueIdentifierToNumber(SystemInfo.deviceUniqueIdentifier);
        mainCam = FindObjectOfType<Camera>();
		if(isLocalPlayer)
        {
            //generate character appearance
            Debug.Log("Calling genereate");
            generate();
            CmdsendDataToServer(uniqueNumber);

            //turn on movement script
            GetComponent<Movement>().enabled = true;
            //GameObject.FindGameObjectWithTag("NPC").GetComponent<DialogEngine>().StartConversation(this.gameObject);

            //set this as the parent of the camera
            mainCam.transform.parent = transform;
            mainCam.transform.position = new Vector3(0, 0, -10);
        }
        GetComponent<SpawnCharacter>().renderPlayer(body, top, legs, eyes, hair, skinC, topC, legsC, shoesC, hairC);
    }


    public void generate()
    {
        Debug.Log("Generate is being called");
        hair = uniqueNumber[1] % 5;
        top = uniqueNumber[2] % 4;
        legs = uniqueNumber[3] % 2;
        eyes = uniqueNumber[4] % 5;
        body = uniqueNumber[5] % 4;
        
        float[] uNum = new float[40];
        for (int i = 6; i < 21; i++)
        {
            uniqueNumber[i] = (uniqueNumber[i] == 0) ? 10 : uniqueNumber[i];
            uNum[i] = 1.0f / uniqueNumber[i];
        }
        
        skinC = new Color(uNum[6], uNum[7], uNum[8], 1);
        topC = new Color(uNum[9], uNum[10], uNum[11], 1);
        legsC = new Color(uNum[12], uNum[13], uNum[14], 1);
        shoesC = new Color(uNum[15], uNum[16], uNum[17], 1);
        hairC = new Color(uNum[18], uNum[19], uNum[20], 1);
    }

    [Command]
    void CmdsendDataToServer(int[] uniqueNumber)
    {
        Debug.Log("sending generate to server");

        hair = uniqueNumber[1] % 5;
        top = uniqueNumber[2] % 4;
        legs = uniqueNumber[3] % 2;
        eyes = uniqueNumber[4] % 5;
        body = uniqueNumber[5] % 4;

        float[] uNum = new float[40];
        for (int i = 6; i < 21; i++)
        {
            uniqueNumber[i] = (uniqueNumber[i] == 0) ? 10 : uniqueNumber[i];
            uNum[i] = 1.0f / uniqueNumber[i];
        }

        skinC = new Color(uNum[6], uNum[7], uNum[8], 1);
        topC = new Color(uNum[9], uNum[10], uNum[11], 1);
        legsC = new Color(uNum[12], uNum[13], uNum[14], 1);
        shoesC = new Color(uNum[15], uNum[16], uNum[17], 1);
        hairC = new Color(uNum[18], uNum[19], uNum[20], 1);

        GetComponent<SpawnCharacter>().renderPlayer(body, top, legs, eyes, hair, skinC, topC, legsC, shoesC, hairC);
    }

    void uniqueIdentifierToNumber(string uniqueIdentifier)
    {
        for (int i = 0; i < 40; i++)
        {
            switch (uniqueIdentifier[i])
            {
                case 'a':
                    uniqueNumber[i] = 1;
                    break;
                case 'b':
                    uniqueNumber[i] = 2;
                    break;
                case 'c':
                    uniqueNumber[i] = 3;
                    break;
                case 'd':
                    uniqueNumber[i] = 4;
                    break;
                case 'e':
                    uniqueNumber[i] = 5;
                    break;
                case 'f':
                    uniqueNumber[i] = 6;
                    break;
                default:
                    uniqueNumber[i] = uniqueIdentifier[i] - 48;
                    break;
            }
        }

    }
}