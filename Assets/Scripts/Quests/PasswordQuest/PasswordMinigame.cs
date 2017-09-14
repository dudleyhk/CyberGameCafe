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
    TYPE_COMMONPASSWORD,
    TYPE_STRONG,
    TYPE_PASSWORD,
    TYPE_SHORT
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
        new passphrase("Password",PassphraseType.TYPE_PASSWORD),
        new passphrase("Hello",PassphraseType.TYPE_STRONG),
        new passphrase("This",PassphraseType.TYPE_STRONG),
        new passphrase("Is",PassphraseType.TYPE_STRONG),
        new passphrase("A Test",PassphraseType.TYPE_STRONG),
    };
}

public class PasswordMinigame : MonoBehaviour {

    public GameObject textButtonToSpawn;
    public GameObject notificationWindow;
    public int numberOfPassphraseOptions = 5;

    private List<passphrase> passwordString;
    private passphrase thePassword;

    private int phraseNo = 0;

    private int passwordScore;
    private string passwordHints = "\0";

    private GameObject passwordMiniGameCollider;

    void Awake()
    {
        GameObject buttonToSpawn;
        passphrase phraseToSet;
        for(int i = 0; i < numberOfPassphraseOptions; i++)
        {
            buttonToSpawn = Instantiate(textButtonToSpawn, gameObject.transform);
            phraseToSet = PasswordPhrases.passphrases[Random.Range(0, PasswordPhrases.passphrases.Length)]; // TODO - make this a function to ensure the phrase choices are diverse.
            buttonToSpawn.GetComponent<Text>().text = phraseToSet.phrase;
            buttonToSpawn.GetComponent<TextButton>().setPassphrase(phraseToSet);
            buttonToSpawn.GetComponent<RectTransform>().anchoredPosition = new Vector2(30 + (60 * i), -54); // TODO - refine text layout system.
            // TODO - Add code to layout the objects. 
        }
    }
	
    public void addPassphraseToPassword(passphrase thePhrase)
    {
        GetComponentInChildren<InputField>().text = thePhrase.phrase;
        thePassword = thePhrase;
        // passwordString.Add(thePhrase);
    }

    public void evaluatePassword()
    {
        GameObject windowToSpawn;
        // Here we check the password and score the users efforts.
        // If its good we can destroy ourselves and allow the user to move again as well as allow the game to continue.
        // else we display an error message and some advice.


        // TODO - evaluate password score and then rispond accordigly.
        if (checkPasswordStrength() != PassphraseType.TYPE_STRONG)
        {
            windowToSpawn = Instantiate(notificationWindow, gameObject.transform);
             GameObject.FindGameObjectWithTag("UIMinigameErrorWindow")
                 .GetComponentInChildren<Text>().text = passwordHints;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<QuestSystem>()
                .updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "passwordCreate");
            passwordMiniGameCollider.GetComponent<PasswordMinigameWindow>().isGameComplete(true);
            Destroy(gameObject);
        }
    }

    PassphraseType checkPasswordStrength()
    {
            switch (thePassword.phraseType)
            {
                case PassphraseType.TYPE_NAME:
                    updatePasswordHints("You shouldnt use names. Those passwords can be guessed easily.");
                    break;
                case PassphraseType.TYPE_PASSWORD:
                    updatePasswordHints("NEVER EVER EVER USE 'PASSWORD' AS YOUR PASSWORD!");
                     break;
                case PassphraseType.TYPE_COMMONPASSWORD:
                    updatePasswordHints("Common words can be easily guessed by password crackers. I would consider using something else.");
                    break;
                case PassphraseType.TYPE_SHORT:
                    updatePasswordHints("The password is quite short. You should choose a pass");
                    break;
            }

        return thePassword.phraseType;
    }

    void updatePasswordHints(string hintToAdd)
    {
        passwordHints = hintToAdd;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR // Only execute the code below if we are testing.
            GameObject.FindGameObjectWithTag("Player").GetComponent<QuestSystem>()
    .updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "passwordCreate");
            passwordMiniGameCollider.GetComponent<PasswordMinigameWindow>().isGameComplete(true);
#endif
            Destroy(gameObject);
        }
    }

    public void setPasswordMiniGameCollider(GameObject theCollider)
    {
        passwordMiniGameCollider = theCollider;
    }
}
