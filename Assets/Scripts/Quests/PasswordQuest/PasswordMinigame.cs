using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Set of password constants that the game will choose from using RNG as the selection for the users passwords.
// Bad and good phrases should be included in here.
// When password options are available we shopuld also be able to choose one of the previous passwords we created. (Doing so causes the quest to fail.)
public class PasswordPhrases
{
    // TODO - Add some passphrases based on the criteria on the class comment. 
    public static string[] passphrases =
    {
        "Hello",
        "Welcome",
        "This",
        "is",
        "a",
        "Test"
    };
}


public class PasswordMinigame : MonoBehaviour {

    public GameObject textButtonToSpawn;
    public int numberOfPassphraseOptions = 5;


    void Awake()
    {
        for(int i = 0; i < numberOfPassphraseOptions; i++)
        {
            textButtonToSpawn.GetComponent<Text>().text = PasswordPhrases.passphrases[Random.Range(0, PasswordPhrases.passphrases.Length)];
            Instantiate(textButtonToSpawn, gameObject.transform);
            // TODO - Add code to layout the objects. 
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
