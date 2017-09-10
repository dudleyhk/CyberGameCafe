using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordMinigame : MonoBehaviour {

    public GameObject MinigameWindow;

    private bool gameInProgress;

    // Starts the Password Minigame Freezing the player unitl they exit. 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!gameInProgress)
        {
            gameInProgress = true;
            GameObject uiCanvas = GameObject.FindGameObjectWithTag("UI");
            GameObject gameWindow = Instantiate(MinigameWindow, uiCanvas.transform);
        }
    }

}
