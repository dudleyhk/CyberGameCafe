using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndTrigger : MonoBehaviour 
{
	public GameObject gameController;

	void OnTriggerEnter2D(Collider2D col)
	{
        GameObject scorer = GameObject.Find("EternalObject");
        if(scorer)
        {
            scorer.GetComponent<EternalScript>().publicPCScore =
                float.Parse(GameObject.FindGameObjectWithTag("Timer")
                .GetComponent<Text>().text);
        }

        gameController.GetComponent<MazeTrigger>().StartOrEndTriggered(col.gameObject);
        Destroy(gameObject);
	}

}