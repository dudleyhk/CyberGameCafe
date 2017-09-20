using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTheBoy : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Destroying the boy");
        Destroy(col.gameObject);
    }
}
