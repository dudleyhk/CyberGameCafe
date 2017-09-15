using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSetup : MonoBehaviour
{
    public int hair;
    public int top;
    public int legs;
    public int eyes;
    public int body;

    public Color skinC;
    public Color hairC;
    public Color topC;
    public Color shoesC;
    public Color legsC;

    private Camera mainCam;

    void Start()
    {
        generate();
        GetComponent<NPCSpawn>().render
            (body, top, legs, eyes, hair, skinC, topC, legsC, shoesC, hairC);
    }


    public void generate()
    {
		hair = Random.Range(0,4);
		top = Random.Range(0,3);
		legs = Random.Range(0,1);
		eyes = Random.Range(0,4);
		body = Random.Range(0,3);

		skinC = getRandoColour ();
		topC = getRandoColour ();
		legsC = getRandoColour();
		shoesC = getRandoColour ();
		hairC = getRandoColour();
    }

	Color getRandoColour()
	{
		float r = 2f / Random.Range (1, 9);
		float g = 2f / Random.Range (1, 9);
		float b = 2f / Random.Range (1, 9);
		return new Color (r,g,b, 1);
	}
}