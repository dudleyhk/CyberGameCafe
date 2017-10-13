using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuse : MonoBehaviour
{
    DialogueMessages d;

    void Start()
    {
        GameObject textBox = GameObject.FindGameObjectWithTag("TextBox");
        d = textBox.GetComponent<DialogueMessages>();
    }

    public void accuse(int accusation)
    {
        d.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        if (accusation == 5)
        {
            d.spawnTextBox("Okay, well take your time and question them again if you need to. Remember, I'm certain that:"
                + "\nONLY ONE of them is guilty\nand\nEach of them is telling ONE TRUTHFUL STATEMENT and ONE LIE", "Janet");
        }
        else if (accusation != 0)
        {
            d.spawnTextBox("No, it couldn't have been " + getName(accusation) + " he was with me at the time...", "Janet");
            d.spawnTextBox("I think you should speak to them all again. Remember, I'm certain that:"
                + "\nONLY ONE of them is guilty\nand\nEach of them is telling ONE TRUTHFUL STATEMENT and ONE LIE", "Janet");

            //decrease score
            GetComponentInParent<ScorePasswordShare>().addAGuess();

        }
        else
        {
            GameObject scorer = GameObject.Find("EternalObject");
            if(scorer)
            {
                scorer.GetComponent<EternalScript>().sharingScore =
                    GetComponentInParent<ScorePasswordShare>().getNumberOfGuesses();
            }

            d.spawnTextBox("Yes, it must have been Jackie, of course, thank you so much for your help!", "Janet");
            d.spawnTextBox("QUEST COMPLETE!");

            GameObject.FindGameObjectWithTag("Player").GetComponent<QuestSystem>().
            updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "jaccuse");
        }
    }

    string getName(int x)
    {
        switch (x)
        {
            case 1:
                return "Jermaine";
            case 2:
                return "Marlon";
            case 3:
                return "Michael";
            case 4:
                return "Tito";
            default:
                return "error";
        }
    }
}