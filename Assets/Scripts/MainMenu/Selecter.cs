using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour {

    GameObject[] listItems;
    int currentParent;
    float rest;

    int getCurrentParent()
    {
        return 0;
    }

    void Start()
    {
        rest = 0;
        currentParent = 0;
        listItems = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            listItems[i] = transform.parent.parent.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (rest <= 0)
        {
            if (verticalInput > 0 && currentParent != 0)
            {
                currentParent--;
                rest = 0.2f;
            }
            if (verticalInput < 0 && currentParent != 2)
            {
                currentParent++;
                rest = 0.2f;
            }

            transform.SetParent(listItems[currentParent].transform, false);
        }
        rest -= Time.deltaTime;

        if(Input.GetButtonDown("Interact"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<MainMenu>().action(currentParent);
        }
    }
}
