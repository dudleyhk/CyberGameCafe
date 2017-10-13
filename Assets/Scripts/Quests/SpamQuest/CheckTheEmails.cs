using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheEmails : MonoBehaviour
{
    GameObject scorer;
    private GameObject player;
    private bool playerInBox;

    private GameObject textBox;

    private int textState;

    private Mission thisQuest;

	private Interact speak;

	void Start()
	{
		speak = GameObject.FindGameObjectWithTag ("GameController").
			GetComponent<Interact>();
        thisQuest = GetComponentInParent<Mission>();
        textBox = GameObject.FindGameObjectWithTag("TextBox");
        textState = 0;

        scorer = GameObject.Find("EternalObject");
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

    void Update()
    {
        if (speak.beingPressed)
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
                d.spawnTextBox("Good afternoon, my name is Martin\nI am an I.T. technician.", "Martin");
                d.spawnTextBox("We are developping a new spam filter AI to use on these computers."
                    + "\nWe need to teach it how to recognise potentially suspicious E-Mails.", "Martin");
                d.spawnTextBox("Please go onto the computer above me and read the E-Mails. For each thing"
                    + " you see that could be an indiation that the E-Mail is phishing spam,"
                    + " click the 'Suspicion' button when it appears.", "Martin");

                player.GetComponent<QuestSystem>().assignMission(thisQuest, gameObject);
                thisQuest.startMission();
            }

            else if (thisQuest.getActiveObjective().getObjectiveTag() == "checkCom")
            {
                d.spawnTextBox("The computer just above me.\nWould you kindly interact with it.", "Martin");
            }
            else if (thisQuest.getActiveObjective().getObjectiveTag() == "findSpam")
            {
                d.spawnTextBox("Sorry, I'm very busy, would you mind checking one more?", "Martin");
            }
            else if (thisQuest.isCompleated())
            {
                if (scorer)
                {
                    int score = GetComponentInParent<Questions>().getScore();
                    int maxScore = GetComponentInParent<Questions>().getMaxScore();
                    d.spawnTextBox("Your score of " + score + " means you get a score for this quest of "
                        + scorer.GetComponent<ConvertScore>().getRealScore(score, 0, maxScore), "Martin");
                }
            }
            else if (thisQuest.getActiveObjective().getObjectiveTag() == "checkAnswers")
            {
                int score = GetComponentInParent<Questions>().getScore();
                int maxScore = GetComponentInParent<Questions>().getMaxScore();

                if (scorer)
                {
                    scorer.GetComponent<EternalScript>().phishingScore = score;
                }

                d.spawnTextBox("The total number of things to find in those E-Mails was "
                    + maxScore + " and you found " + score, "Martin");
                string feedback;
                if (score == maxScore)
                {
                    feedback = "You did wonderfully, great job!"
                        + "\nHere are a few more things to look out for when you check your E-Mails.";
                    player.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "checkAnswers");
                }
                else if (score > maxScore / 2)
                {
                    feedback = "You have really helped us out today, thanks."
                        + "\nHere are a few things to look out for when you check your E-Mails.";
                    player.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "checkAnswers");
                }
                else
                {
                    feedback = "I think maybe you should try again some time"
                         + "\nHere are a few things to look out for when you check your E-Mails.";
                    player.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "checkAnswers");
                }
                d.spawnTextBox(feedback, "Martin");
                for (int i = 0; i < 3; i++)
                {
                    d.spawnTextBox(GetComponentInParent<ThingsToRemember>().getMissedAnswer(), "Martin");
                }
            }
        }
    }
}