using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInPlayer : MonoBehaviour {

	[SerializeField]
	private GameObject player;
	private GameObject camera;

	// Use this for initialization
	void Start () 
	{
		GameObject playerClone = Instantiate (player);
        playerClone.transform.position = new Vector3(29, 26, 0);
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (camera.transform.parent != null) 
		{
			camera.transform.parent = null;
			camera.transform.Translate (9, 4.857f, 0);
			
		}
	}
}
