using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncryptionMiniGames : MonoBehaviour {

    public GameObject unencryptedText;
    public InputField userInputField;
	public Button sendButton;
	public Text scoreText;

    private Text originalText;
    private string correctText = null;
    private bool sendPressed = false;
    private int increaseTextBy = 0;

    // Use this for initialization
    void Start ()
    {
        originalText = unencryptedText.GetComponent<Text>();
        sendButton.onClick.AddListener(sendButtonPressed);
        increaseTextBy = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        originalText.text = "Bristol is the worlds best city";
        converting();

		if (sendPressed == true) 
		{
            sendPressed = false;
            Debug.Log("Button Pressed");
			markPlayerInput ();
		}

	}

    void converting()
    {
        string convertingOriginal = originalText.text;
        for (int i = 0; i < originalText.text.Length; i++)
        {
            char characterStorage = convertingOriginal[i];

            if (increaseTextBy == 1)
            {
                characterStorage++;
            }
            if (increaseTextBy == 3)
            {
                characterStorage++;
                characterStorage++;
                characterStorage++;
            }
            if (increaseTextBy == -2)
            {
                characterStorage--;
                characterStorage--;
            }

            if (characterStorage == '!')
            {
                characterStorage = ' ';
            }

            convertingOriginal = convertingOriginal.Remove(i, 1);
            convertingOriginal = convertingOriginal.Insert(i, characterStorage.ToString());
        }
        correctText = convertingOriginal;
    }

    void markPlayerInput()
    {
        Debug.Log(correctText);
        int score = 0; 

        for (int i = 0; i < correctText.Length; i++)
		{
			if (userInputField.text[i] == correctText[i])
			{
				score++;
			}
		}
        float percentageCorrect = 0;
        float floatScore = score;
        float floatMax = correctText.Length;
        percentageCorrect = (floatScore / floatMax) *100;

        Debug.Log(score);
        Debug.Log(correctText.Length);
        Debug.Log(percentageCorrect);
        scoreText.text = score.ToString() + " out of " + correctText.Length + " " + percentageCorrect + "%";
    }

    void sendButtonPressed()
    {
        sendPressed = true;
    }
}
