using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncryptionMiniGames : MonoBehaviour
{

    public GameObject unencryptedText;
    public InputField userInputField;
    public Button sendButton;
    public Text scoreText;
    public Text descriptionText;
    public Text buttonText;

    private Text originalText;
    private string correctText = null;
    private bool sendPressed = false;
    private int increaseTextBy = 0;
    private int level = 1;

    // Use this for initialization
    void Start()
    {
        originalText = unencryptedText.GetComponent<Text>();
        sendButton.onClick.AddListener(sendButtonPressed);
        originalText.text = "placeholder";
        converting();
    }

    // Update is called once per frame
    void Update()
    {
        if (sendPressed == true)
        {
            sendPressed = false;
            level++;
            markPlayerInput();
            converting();
        }

        if(level > 3)
        {
            buttonText.text = "Exit";
        }
    }

    void converting()
    {
        increaseTextBy = Levels(level);
        string convertingOriginal = originalText.text;
        for (int i = 0; i < originalText.text.Length; i++)
        {
            char characterStorage = convertingOriginal[i];

            if (increaseTextBy > 0)
            {
                for (int j = 0; j < increaseTextBy; j++)
                {
                    if (characterStorage != ' ')
                    {
                        if (characterStorage == 'z')
                        {
                            characterStorage = 'a';
                        }
                        else
                        {
                            characterStorage++;
                        }
                    }
                }
            }
            if (increaseTextBy < 0)
            {
                for (int j = 0; j > increaseTextBy; j--)
                {

                    if (characterStorage != ' ')
                    {
                        if (characterStorage == 'a')
                        {
                            characterStorage = 'z';
                        }
                        else
                        {
                            characterStorage--;
                        }
                    }

                    
                }
            }

            convertingOriginal = convertingOriginal.Remove(i, 1);
            convertingOriginal = convertingOriginal.Insert(i, characterStorage.ToString());
        }
        correctText = convertingOriginal;
        Debug.Log(correctText);
    }

    void markPlayerInput()
    {
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
        percentageCorrect = (floatScore / floatMax) * 100;

        //Debug.Log(score);
        //Debug.Log(correctText.Length);
        //Debug.Log(percentageCorrect);
        scoreText.text = score.ToString() + " out of " + correctText.Length + " " + percentageCorrect + "%";
    }

    void sendButtonPressed()
    {
        sendPressed = true;

        if (level > 3)
        {
            Application.LoadLevel("SinglePlayer");
        }
    }

    int Levels(int levelNumber)
    {
        int changeTextBy = 0;

        switch (levelNumber)
        {
            case 1:
                changeTextBy = 1;
                originalText.text = "you forgot to log out of this computer";
                break;
            case 2:
                changeTextBy = 3;
                originalText.text = "someone stole all of your bank details";
                break;
            case 3:
                changeTextBy = -2;
                originalText.text = "because your uni password for everything";
                break;
            default:
                changeTextBy = 0;
                break;
        }

        Debug.Log(changeTextBy);

        string descriptionStringText = descriptionText.text;
        descriptionStringText = descriptionStringText.Remove(16, 1);
        descriptionStringText = descriptionStringText.Insert(16, changeTextBy.ToString());
        descriptionStringText = descriptionStringText.Remove(32, 1);
        descriptionStringText = descriptionStringText.Insert(32, changeTextBy.ToString());
        descriptionText.text = descriptionStringText;

        return changeTextBy;
    }
}