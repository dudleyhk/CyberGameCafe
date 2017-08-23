using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMessages : MonoBehaviour
{
    public void spawnTextBox(string text)
    {
        //hide the UI
        showOrHideUI(false);

        //show a dialogue box
        GetComponent<Button>().onClick.AddListener(advanceText);
        GetComponentInChildren<Text>().text = text;
        GetComponent<Image>().enabled = true;
        GetComponent<Button>().enabled = true;
        GetComponentInChildren<Text>().enabled = true;
    }

    void advanceText()
    {
        //show UI again
        showOrHideUI(true);

        //destroy the text box
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
    }

    void showOrHideUI(bool b)
    {
        //Debug.Log(b ? "Enabling the things" : "Disabling the things");

        GameObject interButton = GameObject.FindGameObjectWithTag("InteractButton");
        interButton.GetComponent<Button>().enabled = b;
        interButton.GetComponent<Image>().enabled = b;
        interButton.GetComponentInChildren<Text>().enabled = b;

        GameObject js = GameObject.FindGameObjectWithTag("Joystick");
        js.GetComponent<VirtualJoysticks>().enabled = b;
        js.GetComponent<Image>().enabled = b;

        GameObject jsChild = js.transform.GetChild(0).gameObject;
        jsChild.GetComponent<Image>().enabled = b;
        jsChild.GetComponentInChildren<Text>().enabled = b;
    }
}
