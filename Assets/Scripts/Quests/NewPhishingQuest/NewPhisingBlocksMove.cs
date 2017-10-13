using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPhisingBlocksMove : MonoBehaviour
{

    float rotationSpeed;
    float fallSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(-5, 5);
        fallSpeed = Random.Range(-0.5f, -2f) / 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(fallSpeed, 0, 0);
        transform.Rotate(0, 0, rotationSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //send them to the shadow realm
            Destroy(col.gameObject);

            GameObject timer = GameObject.FindGameObjectWithTag("Timer");
            float score = float.Parse(timer.GetComponent<Text>().text);

            GameObject scorer = GameObject.Find("EternalObject");
            if(scorer)
            {
                scorer.GetComponent<EternalScript>().USBScore = score;
            }

            Application.LoadLevel("SinglePlayer");
        }
    }
}
