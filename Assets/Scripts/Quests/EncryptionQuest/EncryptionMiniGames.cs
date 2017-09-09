using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncryptionMiniGames : MonoBehaviour {

    public GameObject unencryptedText;
    public InputField userInputField;

    private Text originalText;
    private string correctText = null;

    // Use this for initialization
    void Start ()
    {
        originalText = unencryptedText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        originalText.text = "Bristol is the worlds best city";
        converting();
	}

    void converting()
    {
        string convertingOriginal = originalText.text;
        for (int i = 0; i < originalText.text.Length; i++)
        {
            char characterStorage = convertingOriginal[i];
            characterStorage++;
            if (characterStorage == '!')
            {
                characterStorage = ' ';
            }

            convertingOriginal = convertingOriginal.Remove(i, 1);
            convertingOriginal = convertingOriginal.Insert(i, characterStorage.ToString());
        }
        correctText = convertingOriginal;
        Debug.Log(correctText);
    }

    void markPlayerInput()
    {

    }
}
