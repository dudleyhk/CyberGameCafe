using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour {

    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().stopMovement();
        this.gameObject.GetComponent<DialogEngine>().StartConversation(GameObject.FindGameObjectWithTag("Player"));
        // this.gameObject.GetComponent<DialogEngine>().printMessage();
    }
}
