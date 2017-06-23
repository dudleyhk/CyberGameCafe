using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertIPToRoomCode : MonoBehaviour
{
    public string ipToCode(string IP)
    {
        string[] ipSegments = new string[4];

        char[] ipChars = new char[3];

        int index = 0;
        //iterate for each ipSegments string
        for(int i = 0; i < 4; i++)
        {
            //set the characters as null
            for(int j = 0; j < 3; j++)
            {
                ipChars[j] = '\0';
            }

            //find the characters in the ip
            for (int j = 0; index < IP.Length; index++, j++)
            {
                //break when a . is found
                if (IP[index] == '.')
                {
                    break;
                }
                ipChars[j] = IP[index];
            }
            index++;

            //set the string to the ipChars and parse it into the int
            ipSegments[i] = new string(ipChars);
            int tempInt;
            int.TryParse(ipSegments[i], out tempInt);

            //convert to a hex string and ensure it's two digits
            ipSegments[i] = tempInt.ToString("x");
            if(ipSegments[i].Length == 1)
            {
                ipSegments[i] = ("0" + ipSegments[i][0]);
            }
        }

        //put the entire thing into a single string to return
        char[] returnString = new char[9];
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                returnString[4 * j + i] = convertChar(ipSegments[i][j]);
            }
        }
        for (int i = 8; i > 4; i--)
        {
            returnString[i] = returnString[i - 1];
        }
        returnString[4] = ' ';

        return new string(returnString);
    }

    public string codeToIp(string code)
    {
        //put the string into chars and back in order
        char[] characters = new char[15];
        for(int i = 0; i < 8; i++)
        {
            characters[i] = convertChar(code[(i / 2) + (4 * (i % 2))]);
        }

        //convert each pair of characters back into an int
        int[] ipFragments = new int[4];
        for(int i = 0; i < 4; i++)
        {
            char[] tempChar = new char[2];
            for(int j = 0; j < 2; j++)
            {
                tempChar[j] = characters[j + (i * 2)];
            }
            ipFragments[i] = int.Parse(new string(tempChar), System.Globalization.NumberStyles.HexNumber);
        }

        //put each int into a string
        string[] ipAsStringFragments = new string[4];
        for(int i = 0; i < 4; i++)
        {
            ipAsStringFragments[i] = ipFragments[i].ToString();
        }

        //put each fragment into a big string with the .s in
        int characterIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < ipAsStringFragments[i].Length; j++)
            {
                characters[characterIndex] = ipAsStringFragments[i][j];
                characterIndex++;
            }
            if (i != 3)
            {
                characters[characterIndex] = '.';
                characterIndex++;
            }
        }

        code = new string(characters);
        return code;
    }

    public char convertChar(char c)
    {
        c = c.ToString().ToUpper()[0];
        switch(c)
        {
            case '0':
                return 'g';
            case 'G':
                return '0';
            case '1':
                return 'h';
            case 'H':
                return '1';
            case '2':
                return 'i';
            case 'I':
                return '2';
            case '3':
                return 'j';
            case 'J':
                return '3';
            case '4':
                return 'k';
            case 'K':
                return '4';
            case '5':
                return 'l';
            case 'L':
                return '5';
            case '6':
                return 'm';
            case 'M':
                return '6';
            case '7':
                return 'n';
            case 'N':
                return '7';
            case '8':
                return 'p';
            case 'P':
                return '8';
            case '9':
                return 'q';
            case 'Q':
                return '9';
            default:
                return c;
        }
    }
}
