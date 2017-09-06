using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PasswordGame : MonoBehaviour {

    // game variables.
    private int gameScore;

    [SerializeField]
    private int passwordEntropy;

    private string password;
    
    public void submitPassword(string newPassword)
    {
        password = newPassword;
    }

    // calcuate the strength of the password. 
    private void calculateEntropy()
    {
        int characterCode = 0;
        int[] minAndMax = new int[2];

        minAndMax[0] = 256;
        minAndMax[1] = 0;

        for (int i = 0; i < password.Length; i++)
        {
            characterCode = ((int)password[i]);

            if (minAndMax[0] > characterCode)
            {
                minAndMax[0] = characterCode;
            }
            else if (minAndMax[1] < characterCode)
            {
                minAndMax[1] = characterCode;
            }

        }

        passwordEntropy = password.Length * ((int)Mathf.Log(minAndMax[1] - minAndMax[0]));
    }
}
