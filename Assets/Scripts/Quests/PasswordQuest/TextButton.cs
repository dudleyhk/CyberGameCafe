using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour {

    private passphrase passwordComponent;
    private bool mouseDown = false;

    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void setPassphrase(passphrase phraseToSet)
    {
        passwordComponent = phraseToSet;
    }

    void OnClick()
    {
        // Add text to the input field.
        gameObject.transform.parent.GetComponent<PasswordMinigame>().addPassphraseToPassword(passwordComponent);
        gameObject.SetActive(false);
    }
}
