using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour 
{

	[SerializeField]
	private BoxCollider2D usbPrefab;
	private int usbCounter;


	// Use this for initialization
	void Start () 
	{
		usbPrefab = gameObject.GetComponent<BoxCollider2D> ();
		usbCounter = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			GetComponentInParent<counterManagerScript>().UpdateCounter();
		}
	}

}
