using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameSelecter : MonoBehaviour {

    GameObject[] listItems;
    int currentParent;
    float rest;
    int currentindex;

    void Start()
    {
        currentindex = 0;
        rest = 0;
        currentParent = 0;
        listItems = new GameObject[29];
        for(int i = 0; i < 29; i++)
        {
            listItems[i] = transform.parent.parent.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (rest <= 0)
        {
            if (verticalInput < 0 && currentParent < 24)
            {
                currentParent += 6;
                rest = 0.2f;
            }

            if (verticalInput > 0 && currentParent > 5)
            {
                currentParent -= 6;
                rest = 0.2f;
            }

            if(horizontalInput > 0 && currentParent % 6 != 5)
            {
                currentParent++;
                rest = 0.2f;
            }

            if(horizontalInput < 0 && currentParent % 6 != 0)
            {
                currentParent--;
                rest = 0.2f;
            }

            transform.SetParent(listItems[currentParent].transform, false);
        }
        rest -= Time.deltaTime;

        if(Input.GetButtonDown("Interact"))
        {
            EternalScript e = GameObject.Find("EternalObject").GetComponent<EternalScript>();

            if (currentParent < 26)
            {
                e.playerName[currentindex] = listItems[currentParent].name[0];
                if (currentindex < 7)
                {
                    currentindex++;
                }
            }
            else if(currentParent == 26)
            {
                currentindex--;
                e.playerName[currentindex] = ' ';
            }
            else if(currentParent == 27)
            {
                e.playerName[currentindex] = ' ';
                currentindex++;
            }
            else if(currentParent == 28)
            {
                Destroy(GameObject.Find("Audio"));
                Application.LoadLevel("SinglePlayer");
            }
            else
            {
                Debug.LogError("Error somehow");
            }

        }
    }
}
