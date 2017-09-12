using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Set of password constants that the game will choose from using RNG as the selection for the users passwords.
// Bad and good phrases should be included in here.
// When password options are available we shopuld also be able to choose one of the previous passwords we created. (Doing so causes the quest to fail.)
public enum PassphraseType
{
    TYPE_NAME = 0,
    TYPE_COMMON,
    TYPE_WIDE_CHARRANGE,
    TYPE_NARROW_CHARRANGE
}

public struct passphrase
{
    public string phrase;
    public PassphraseType phraseType;

    public passphrase(string newPhrase, PassphraseType wordType)
    {
        phrase = newPhrase;
        phraseType = wordType;
    }
}

public class PasswordPhrases
{
    // TODO - Add some passphrases based on the criteria on the class comment. 
    public static passphrase[] passphrases =
    {
        new passphrase("Hello",PassphraseType.TYPE_COMMON),
        new passphrase("This",PassphraseType.TYPE_COMMON),
        new passphrase("Is",PassphraseType.TYPE_COMMON),
        new passphrase("A Test",PassphraseType.TYPE_COMMON),
    };
}

public class PasswordMinigame : MonoBehaviour {

    public GameObject textButtonToSpawn;
    public int numberOfPassphraseOptions = 5;

    private passphrase[] passwordString = new passphrase[3];
    private int phraseNo = 0; 

    void Awake()
    {
        GameObject buttonToSpawn;
        passphrase phraseToSet;
        for(int i = 0; i < numberOfPassphraseOptions; i++)
        {
            buttonToSpawn = Instantiate(textButtonToSpawn, gameObject.transform);
            phraseToSet = PasswordPhrases.passphrases[Random.Range(0, PasswordPhrases.passphrases.Length)];
            buttonToSpawn.GetComponent<Text>().text = phraseToSet.phrase;
            buttonToSpawn.GetComponent<TextButton>().setPassphrase(phraseToSet);
            // TODO - Add code to layout the objects. 
        }
    }
	
    public void addPassphraseToPassword(passphrase thePhrase)
    {
        GetComponentInChildren<InputField>().text += thePhrase.phrase;
        passwordString[phraseNo] = thePhrase;
    }

    public void evaluatePassword()
    {
        // Here we check the password and score the users efforts.
        Debug.Log("Your password is bad.");
    }

    void checkPasswordStrength(string passwordToCheck)
    {
        // TODO - impliment scoring system.
    }
}
