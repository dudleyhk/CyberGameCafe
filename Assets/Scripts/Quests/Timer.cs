using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    float timer;
    bool go;
    
	void Awake ()
    {
        go = true;
        timer = 0f;
	}

    void Update()
    {
        if (go)
        {
            timer += Time.deltaTime;
            GetComponent<Text>().text = ((int)(timer * 10) / 10f).ToString();
        }
    }

    public float getTime()
    {
        return timer;
    }

    public void penalty(float amount)
    {
        timer += amount;
    }
}