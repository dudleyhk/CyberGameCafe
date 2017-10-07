using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePasswordShare : MonoBehaviour {

    int numberOfGuesses = 0;

    public void addAGuess()
    {
        numberOfGuesses++;
    }

    public int getNumberOfGuesses()
    {
        return numberOfGuesses;
    }
}