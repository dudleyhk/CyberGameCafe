using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurePasswordScore : MonoBehaviour {

    int score;

	// Use this for initialization
	void Start ()
    {
        score = 0;
	}

    public void increaseScore()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }
}
