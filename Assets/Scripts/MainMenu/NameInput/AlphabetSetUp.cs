using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetSetUp : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Text a = GetComponent<Text>();
        a.text = gameObject.name;
    }
}
