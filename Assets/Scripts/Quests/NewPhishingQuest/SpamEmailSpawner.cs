using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamEmailSpawner : MonoBehaviour {

    //public int numberOfPrefabs = 3;
    public GameObject[] thingsToDodge = new GameObject[3];
    private GameObject spawnedObject;
    float delay = 0.0f;
    float howLong = 0; 

    // Use this for initialization
    void Start ()
    {
        howLong = Random.Range(1.5f, 2.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (delay > howLong)
        {
            spawnedObject = Instantiate(thingsToDodge[0]);
            spawnedObject.transform.position = gameObject.transform.position;
            spawnedObject.transform.rotation = new Quaternion(0, 0, Random.Range(0, 359), 0);
            delay = 0;

            howLong = Random.Range(1.5f, 2.5f);
        }

        delay += Time.deltaTime;
    }
}
