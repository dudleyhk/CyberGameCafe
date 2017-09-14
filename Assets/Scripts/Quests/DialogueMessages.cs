using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMessages : MonoBehaviour
{
    private List<string> textBoxes = new List<string>();
	Text speaker;

    void Awake()
    {
		speaker = transform.GetChild (1).GetComponent<Text> ();
        GetComponent<Button>().onClick.AddListener(advanceText);
    }

	public void switchButton(bool on)
	{
		GetComponent<Button> ().enabled = on;
	}

	public void spawnTextBox(string text, string speakerName = "")
    {

		speaker.text = speakerName;
		if (speakerName != "") {
			speaker.text = speaker.text + ":";
		}
		speaker.enabled = true;

        GameObject.FindGameObjectWithTag("Player").
        GetComponent<Movement>().stopMovement();

        //and to add a bit that prevents the player from pressing e

        textBoxes.Add(text);

        if (!GetComponent<Image>().enabled)
        {
            //hide the UI
            showOrHideUI(false);

            //show a dialogue box
            showText();
            GetComponent<Image>().enabled = true;
            GetComponent<Button>().enabled = true;
            GetComponentInChildren<Text>().enabled = true;
        }
    }

    void showText()
    {
        GetComponentInChildren<Text>().text = textBoxes[0];
    }

    void advanceText()
    {
        if (textBoxes.Count > 0)
        {
            textBoxes.RemoveAt(0);
        }

        if (textBoxes.Count == 0)
        {
			turnOffBox ();
        }
        else
        {
            showText();
        }
    }

	public void turnOffBox()
	{
		//show UI again
		showOrHideUI(true);
		//destroy the text box
		GetComponent<Image>().enabled = false;
		GetComponent<Button>().enabled = false;
		GetComponentInChildren<Text>().enabled = false;
		GameObject.FindGameObjectWithTag("Player").
		GetComponent<Movement>().startMovement();
		speaker.enabled = false;
	}

    void showOrHideUI(bool b)
    {
        //Debug.Log(b ? "Enabling the things" : "Disabling the things");

        GameObject interButton = GameObject.FindGameObjectWithTag("InteractButton");
        if (interButton != null)
        {
            interButton.GetComponent<Button>().enabled = b;
            interButton.GetComponent<Image>().enabled = b;
            interButton.GetComponentInChildren<Text>().enabled = b;
        }

        GameObject js = GameObject.FindGameObjectWithTag("Joystick");
        if (js != null)
        {
            js.GetComponent<VirtualJoysticks>().enabled = b;
            js.GetComponent<Image>().enabled = b;

            GameObject jsChild = js.transform.GetChild(0).gameObject;
            jsChild.GetComponent<Image>().enabled = b;
            jsChild.GetComponentInChildren<Text>().enabled = b;
        }
    }
}