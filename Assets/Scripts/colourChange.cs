using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

public class colourChange : MonoBehaviour
{
    int[] uniqueNumber = new int[9];

    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject hands;
    [SerializeField]
    private GameObject legs;
    

    public Color skinC;
    public Color hairC;
    public Color topC;
    public Color shoesC;
    public Color legsC;

    void Start()
    {
        GameObject forever = GameObject.Find("EternalObject");
        if (forever)
        {
            string x = forever.GetComponent<EternalScript>()
                .playerName.ToString().ToUpper();
            nameToNumber(x);
        }
        else
        {
            uniqueIdentifierToNumber(SystemInfo.deviceUniqueIdentifier);
        }
        //generate character appearance
        generate();
      
    }

    public void generate()
    {
        float[] uNum = new float[9];
        for (int i = 0; i < 9; i++)
        {
            uniqueNumber[i] = (uniqueNumber[i] == 0) ? 10 : uniqueNumber[i];
            uNum[i] = 1.0f / uniqueNumber[i];
        }

        skinC = new Color(uNum[0], uNum[1], uNum[2], 1);
        topC = new Color(uNum[3], uNum[4], uNum[5], 1);
        legsC = new Color(uNum[6], uNum[7], uNum[8], 1);

        head.GetComponent<SpriteRenderer>().color = skinC;
        hands.GetComponent<SpriteRenderer>().color = skinC;
        body.GetComponent<SpriteRenderer>().color = topC;
        legs.GetComponent<SpriteRenderer>().color = legsC;

    }

    void nameToNumber(string name)
    {
        int bigInt = 0;
        for (int i = 0; i < 8; i++)
        {
            bigInt += (int)(getCharValue(name[i]) * Mathf.Pow(27, i));
        }
        for (int i = 0; i < 9; i++)
        {
            uniqueNumber[i] = bigInt % (int)Mathf.Pow(10, i + 1);
            uniqueNumber[i] = uniqueNumber[i] / (int)Mathf.Pow(10, i);
            
        }
    }

    int getCharValue(char c)
    {
        if(c == ' ')
        {
            return 0;
        }
        else
        {
            return (c - 64);
        }
    }

    void uniqueIdentifierToNumber(string uniqueIdentifier)
    {
        for (int i = 0; i < 9; i++)
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
