using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFallers : MonoBehaviour {

    [SerializeField]
    GameObject NPCPrefab;
    GameObject NPCClone;

    float delay = 0;

	// Use this for initialization
	void Update ()
    {
        if (delay > 0.25f)
        {
            NPCClone = Instantiate(NPCPrefab);
            NPCClone.transform.localScale *= 6;
            NPCClone.transform.position = new Vector3(Random.Range(-10, -100), Random.Range(-15, 15), 0);
            NPCClone.transform.rotation = new Quaternion(0, 0, Random.Range(0, 359), 0);
            delay = 0;
        }

        delay += Time.deltaTime;
	}
}
