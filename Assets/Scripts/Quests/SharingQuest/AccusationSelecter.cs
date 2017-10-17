using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccusationSelecter : MonoBehaviour
{
    [SerializeField]
    GameObject janet;

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
        currentParent = 5;
        listItems = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            listItems[i] = transform.parent.parent.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical") + Input.GetAxis("StickVertical");
        float horizontalInput = Input.GetAxis("Horizontal") + Input.GetAxis("StickHorizontal");

        if (rest <= 0)
        {
            if (verticalInput > 0 && currentParent > 2)
            {
                currentParent -= 3;
                rest = 0.2f;
            }
            if (verticalInput < 0 && currentParent <= 2)
            {
                currentParent += 3;
                rest = 0.2f;
            }
            if(horizontalInput > 0 && (currentParent != 2 && currentParent != 5))
            {
                currentParent++;
                rest = 0.2f;
            }
            if(horizontalInput < 0 && (currentParent != 0 && currentParent != 3))
            {
                currentParent--;
                rest = 0.2f;
            }
            
            transform.SetParent(listItems[currentParent].transform, false);
        }
        rest -= Time.deltaTime;

        if (Input.GetButtonDown("Interact"))
        {
            janet.GetComponent<Accuse>().accuse(currentParent);
        }
    }
}
