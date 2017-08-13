using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerStartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private GameObject mainCam;

    // Use this for initialization
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        Instantiate(player);
        player.GetComponent<Movement>().enabled = true;
        mainCam.GetComponent<Camera>().orthographicSize = 5;
        FindObjectOfType<Canvas>().enabled = true;
    }

}