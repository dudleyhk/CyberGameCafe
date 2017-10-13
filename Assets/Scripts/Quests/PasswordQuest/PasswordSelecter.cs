using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSelecter : MonoBehaviour {

    GameObject[] listItems;
    int currentParent;
    float rest;
    public bool thingOff;

    int getCurrentParent()
    {
        return currentParent;
    }

    void Start()
    {
        thingOff = true;
        rest = 0;
        currentParent = 0;
        listItems = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            listItems[i] = transform.parent.parent.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Horizontal");

        if (rest <= 0)
        {
            if (verticalInput < 0 && currentParent != 0)
            {
                currentParent--;
                rest = 0.2f;
            }
            if (verticalInput > 0 && currentParent != 3)
            {
                currentParent++;
                rest = 0.2f;
            }

            transform.SetParent(listItems[currentParent].transform, false);
        }
        rest -= Time.deltaTime;

        if (Input.GetButtonDown("Interact") && thingOff)
        {
            GetComponentInParent<TextButton>().OnClick();
            gameObject.transform.parent.parent.parent
                .GetComponent<PasswordMinigame>().evaluatePassword();
            thingOff = false;
        }
    }
}
