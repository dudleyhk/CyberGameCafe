using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMsgBox : MonoBehaviour {

    //void Awake()
    //{
    //    GetComponent<Button>().onClick.AddListener(OnClick);
    //}

    void OnClick()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
