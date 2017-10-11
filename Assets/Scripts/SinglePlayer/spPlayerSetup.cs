using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spPlayerSetup : MonoBehaviour
{
    int[] uniqueNumber = new int[40];


	[SerializeField]
	private GameObject demon;
	[SerializeField]
	private GameObject panda;

    private Camera mainCam;

    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        //generate character appearance

        //turn on movement script
        GetComponent<Movement>().enabled = true;

        //set this as the parent of the camera
        mainCam.transform.parent = transform;
        mainCam.transform.position = new Vector3
            (transform.position.x, transform.position.y, -10);

		if (Random.Range (0, 2) == 0) 
		{
			Instantiate (demon, transform);			
		} 
		else
		{
            //Instantiate (panda, transform);
            Instantiate(demon, transform);
        }

		//LoadGame lg = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LoadGame> ();
		//if(lg)
		//{
		//	lg.loadGame ();
		//}

    }


}