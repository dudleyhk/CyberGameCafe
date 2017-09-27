using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMoving : MonoBehaviour {

    float direction;
    float speed;

    void Start()
    {
        speed = 2f;
        direction = Random.Range(0f, Mathf.PI * 2);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        switch(c.gameObject.tag)
        {
            case "xWall":
                direction = ((Mathf.PI / 2) - direction) + (Mathf.PI / 2);
                break;
            case "yWall":
                direction = ((Mathf.PI * 2) - direction);
                break;
            case "Letter":
            case "CorrectLetter":
                speed *= -1;
                break;
            case "Player":
                if (gameObject.tag == "CorrectLetter")
                {
                    Debug.Log("Pass");
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<Spawn>().newLetter();
                }
                else
                {
                    Debug.Log("Fail");
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        float x = speed * Mathf.Cos(direction);
        float y = speed * Mathf.Sin(direction);
        transform.Translate(x, y, 0);
    }

    void setSpeed(float s)
    {
        speed = s;
    }
}