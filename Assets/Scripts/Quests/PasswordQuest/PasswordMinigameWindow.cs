using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - change gameInProgress to be a vairable to check if the minigame for that computer is complete. If so the user should not trigger the minigame again. 

public class PasswordMinigameWindow : MonoBehaviour {

    public GameObject MinigameWindow;
    public GameObject questStatusWindow;

    private GameObject gameWindow;
    private bool gameComplete = false;

    [SerializeField]
    private Mission activeMission;

    // Starts the Password Minigame Freezing the player and starting the minigame.
    // Check that the quest is active.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if ((activeMission.getActiveObjective().getObjectiveTag() == "passwordCreate") && (!gameComplete)/* && Input.GetKey(KeyCode.E)*/)
            {
                openMiniGameWindow();
                col.GetComponent<Movement>().stopMovement();
            }
        }
    }
    
    void openMiniGameWindow()
    {
        GameObject uiCanvas = GameObject.FindGameObjectWithTag("UI");
        gameWindow = Instantiate(MinigameWindow, uiCanvas.transform);
        gameWindow.GetComponent<PasswordMinigame>().setPasswordMiniGameCollider(gameObject);
    }

    public void isGameComplete(bool value)
    {
        gameComplete = value;
        //questStatusWindow.GetComponent<QuestStatus>().ToggleCompleationState("Quest 1");
    }
}
