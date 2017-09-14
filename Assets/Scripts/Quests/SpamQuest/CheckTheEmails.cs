using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheEmails : MonoBehaviour
{
    private GameObject player;
    private bool playerInBox;

    private GameObject textBox;

    private int textState;

    private Mission thisQuest;

    void Start()
    {
        thisQuest = transform.parent.gameObject.GetComponent<Mission>();
        textBox = GameObject.FindGameObjectWithTag("TextBox");
        textState = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            playerInBox = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            playerInBox = false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
    }

    void interact()
    {
        if (playerInBox)
        {
            DialogueMessages d = textBox.GetComponent<DialogueMessages>();
            if (thisQuest.getActiveObjective() == null)
            {
                d.spawnTextBox("Good afternoon, my name is Martin\nI am an I.T. technician.");
                d.spawnTextBox("We are developping a new spam filter AI to use on these computers."
                    + "\nWe need to teach it how to recognise potentially suspicious E-Mails.");
                d.spawnTextBox("Please go onto the computer above me and read the E-Mails. Every time you"
                    + "see something that could be an indiation that the E-Mail is phishing spam,"
                    + " click the 'Suspicion' button");

                player.GetComponent<QuestSystem>().assignMission(thisQuest);
                thisQuest.startMission();
            }

            else if (thisQuest.getActiveObjective().getObjectiveTag() == "checkCom")
            {
                d.spawnTextBox("The computer just above me.\nWould you kindly interact with it.");
            }
            else if (thisQuest.getActiveObjective().getObjectiveTag() == "findSpam")
            {
                d.spawnTextBox("Sorry, I'm very busy, would you mind checking one more?");
            }
            else if (thisQuest.getActiveObjective().getObjectiveTag() == "checkAnswers")
            {
                int score = GetComponentInParent<Questions>().getScore();
                int maxScore = GetComponentInParent<Questions>().getMaxScore();
                d.spawnTextBox("The total number of things to find in those E-Mails was "
                    + maxScore + " and you found " + score);
                string feedback;
                if (score == maxScore)
                {
                    feedback = "You did wonderfully, great job!"
                        + "\nHere are a few more things to look out for when you check your E-Mails.";
                    thisQuest.updateActiveMissionObjectives(MissionObjectiveTypes.OBJ_EVENT, "checkAnswers");
                    thisQuest.compleated = true;
                }
                else if (score > maxScore / 2)
                {
                    feedback = "You have really helped us out today, thanks."
                        + "\nHere are a few things to look out for when you check your E-Mails.";
                    thisQuest.updateActiveMissionObjectives(MissionObjectiveTypes.OBJ_EVENT, "checkAnswers");
                    thisQuest.compleated = true;
                }
                else
                {
                    feedback = "I think maybe you should try again"
                         + "\nHere are a few things to look out for when you check your E-Mails.";
                    //complete and restart the mission
                    thisQuest.resetMission();

                    player.GetComponent<QuestSystem>().assignMission(thisQuest);
                    GetComponentInParent<Questions>().resetScore();
                }
                d.spawnTextBox(feedback);
                for (int i = 0; i < 3; i++)
                {
                    d.spawnTextBox(GetComponentInParent<ThingsToRemember>().getMissedAnswer());
                }
                if (score > maxScore / 2)
                {
                    d.spawnTextBox("QUEST COMPLETE!");
                }
            }
        }
    }
}