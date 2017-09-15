using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

public class colourChange : MonoBehaviour
{
    int[] uniqueNumber = new int[40];

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
        uniqueIdentifierToNumber(SystemInfo.deviceUniqueIdentifier);        
        //generate character appearance
        generate();
      
    }

    public void generate()
    {
        float[] uNum = new float[40];
        for (int i = 6; i < 21; i++)
        {
            uniqueNumber[i] = (uniqueNumber[i] == 0) ? 10 : uniqueNumber[i];
            uNum[i] = 1.0f / uniqueNumber[i];
        }

        skinC = new Color(uNum[6], uNum[7], uNum[8], 1);
        topC = new Color(uNum[9], uNum[10], uNum[11], 1);
        legsC = new Color(uNum[12], uNum[13], uNum[14], 1);

        head.GetComponent<SpriteRenderer>().color = skinC;
        hands.GetComponent<SpriteRenderer>().color = skinC;
        body.GetComponent<SpriteRenderer>().color = topC;
        legs.GetComponent<SpriteRenderer>().color = legsC;

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
