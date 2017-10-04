//this script converts scores on each minigame into a number between 0 and 100


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertScore : MonoBehaviour {

    public int getRealScore(float score, float lowThreshold = 0, float highThreshold = 100, bool highIsBad = false)
    {
        float scoreAsPercentage;

        //if it's low enough give it 0
        if (score < lowThreshold)
        {
            scoreAsPercentage = 0;
        }

        //if it's high enough give it 1
        else if (score > highThreshold)
        {
            scoreAsPercentage = 1;
        }

        //otherwise give it a spot between 0 and 1
        else
        {
            scoreAsPercentage = (score - lowThreshold) / (highThreshold - lowThreshold);
        }
        
        return highIsBad ? 100 - (int)(scoreAsPercentage * 100) : (int)(scoreAsPercentage * 100);
    }
}
