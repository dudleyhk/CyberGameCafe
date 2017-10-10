using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTheBoy : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Destructable")
        {
            Debug.Log("Destroying the boy");
            Destroy(col.gameObject);
        }
    }
}
