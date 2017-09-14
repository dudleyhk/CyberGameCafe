using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoAClick : MonoBehaviour {

	[SerializeField]
	GameObject janet;
	int accusation;


	// Use this for initialization
	void Start ()
	{
		GetComponent<Button> ().onClick.AddListener (getIndex);
	}

	void getIndex()
	{
		GameObject textBox = GameObject.FindGameObjectWithTag ("TextBox");
		DialogueMessages d = textBox.GetComponent<DialogueMessages> ();
		accusation = (int)gameObject.name [3] - 48;
		transform.parent.gameObject.SetActive(false);

		d.switchButton (true);

		textBox.GetComponent<Image>().enabled = false;
		textBox.GetComponent<Button>().enabled = false;
		textBox.GetComponentInChildren<Text>().enabled = false;

		if (accusation == 0)
		{
			d.spawnTextBox ("Okay, well take your time and question them again if you need to. Remember, I'm certain that:\n"
				+ "Only one of them is guilty\nand\nEach of them is telling one truthful statement and one lie", "Janet");
		}
		else if (accusation != 1)
		{
			d.spawnTextBox ("No, it couldn't have been " + getName(accusation) + " he was with me at the time...", "Janet");
			d.spawnTextBox ("I think you should speak to them all again. Remember, I'm certain that:\n"
				+ "Only one of them is guilty\nand\nEach of them is telling one truthful statement and one lie", "Janet");
		} 
		else
		{
			d.spawnTextBox ("Yes, it must have been Jackie, of course, thank you so much for your help!", "Janet");
			d.spawnTextBox ("QUEST COMPLETE!");

			Mission thisQuest = janet.transform.parent.gameObject.GetComponent<Mission> ();
			thisQuest.updateActiveMissionObjectives (MissionObjectiveTypes.OBJ_EVENT, "jaccuse");
			thisQuest.setCompleated(true);
		}
	}

	string getName(int x)
	{
		switch (x)
		{
		case 2:
			return "Jermaine";
		case 3:
			return "Marlon";
		case 4:
			return "Tito";
		case 5:
			return "Michael";
		default:
			return "error";
		}
	}
}
