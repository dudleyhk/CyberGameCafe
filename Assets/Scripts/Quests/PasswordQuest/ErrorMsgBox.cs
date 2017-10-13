using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMsgBox : MonoBehaviour {

    //void Awake()
    //{
    //    GetComponent<Button>().onClick.AddListener(OnClick);
    //}

    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            OnClick();
        }
    }

    void OnClick()
    {
        GameObject.FindGameObjectWithTag("PSelecter").GetComponent<PasswordSelecter>().thingOff = true;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
