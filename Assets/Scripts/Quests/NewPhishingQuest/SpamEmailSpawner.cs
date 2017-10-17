using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamEmailSpawner : MonoBehaviour {

    //public int numberOfPrefabs = 3;
    public GameObject[] thingsToDodge = new GameObject[3];
    private GameObject spawnedObject;
    float delay = 0.0f;
    float howLong = 0;

    float howLongMax;
    float howLongMin;

    // Use this for initialization
    void Start ()
    {
        howLongMax = 10f;
        howLongMin = 4f;
        howLong = Random.Range(2f, 10f);
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

            howLongMin = (howLongMin > 1f) ? howLongMin * 0.95f : 1f;
            howLongMax = (howLongMax > 2f) ? howLongMax * 0.9f : 2f;
            howLong = Random.Range(howLongMin, howLongMax);
        }

        delay += Time.deltaTime;
    }
}
