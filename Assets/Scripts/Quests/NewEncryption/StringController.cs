using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringController : MonoBehaviour
{
    [SerializeField]
    string message;

    [SerializeField]
    GameObject showMessage;

    void Start()
    {
        GetComponent<Spawn>().setMessage(message.ToUpper());
        GetComponent<Spawn>().spawn();
        showMessage.GetComponent<Text>().text = message;
    }
}
