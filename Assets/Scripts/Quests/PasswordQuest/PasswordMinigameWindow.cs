using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordMinigameWindow : MonoBehaviour {

    public GameObject MinigameWindow;

    private GameObject gameWindow;
    private bool gameInProgress;

    private Mission activeMission;

    // Starts the Password Minigame Freezing the player and starting the minigame.
    // Check that the quest is active.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            activeMission = col.gameObject.GetComponent<QuestSystem>().getActiveMission();

            if (activeMission != null)
            {
               if ((activeMission.getActiveObjective().getObjectiveTag() == "passwordCreate") && (!gameInProgress))
               {
                    gameInProgress = true;
                    openMiniGameWindow();
               }
            }
        }
    }


    void openMiniGameWindow()
    {
        GameObject uiCanvas = GameObject.FindGameObjectWithTag("UI");
        gameWindow = Instantiate(MinigameWindow, uiCanvas.transform);
    }
}
