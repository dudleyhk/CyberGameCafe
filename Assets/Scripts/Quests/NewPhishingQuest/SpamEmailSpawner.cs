using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamEmailSpawner : MonoBehaviour {

    public GameObject[] thingsToDodge = new GameObject[3];
    private GameObject spawnedObject;
    float delay = 0.0f;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (delay > 0.25f)
        {
            spawnedObject = Instantiate(thingsToDodge[0]);
            spawnedObject.transform.position = gameObject.transform.position;
            spawnedObject.transform.rotation = new Quaternion(0, 0, Random.Range(0, 359), 0);
            delay = 0;
        }

        delay += Time.deltaTime;
    }
}
