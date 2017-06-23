using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GUIManager : MonoBehaviour
{
    public NetworkManager manager = null;
    public GameObject gameController;
   // private NetworkManager manager;
    private Canvas mainCanvas;
    private string roomCode = "";

    void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        // networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        // manager = networkManager.GetComponent<NetworkManager>();
        // manager.networkAddress = Network.player.ipAddress;
        manager.networkAddress = Network.player.ipAddress;
        Debug.Log("Manager IP Address: "  + manager.networkAddress);

    }

    void OnGUI()
    {
        //if it's neither a server nor a client, show this
        //currently shows options to become client or server
        if (!NetworkClient.active && !NetworkServer.active)
        {
            GUI.Label(new Rect(10, 80, 100, 25), "Room Code:");
            roomCode = GUI.TextArea(new Rect(10, 100, 100, 25), roomCode, 8);
            if (GUI.Button(new Rect(10, 128, 100, 25), "Join Game"))
            {
                manager.networkAddress = gameController.GetComponent<ConvertIPToRoomCode>().codeToIp(roomCode);
                
                //if they click to become a client set the camera size to 5 and enable the canvas
                manager.StartClient();
                GetComponent<Camera>().orthographicSize = 5;
                mainCanvas.enabled = true;
            }
            if (GUI.Button(new Rect(0, Screen.height - 25, 100, 25), "Start Server"))
            {
                //begin the server
                manager.StartServer();
                //set the camera size for the server
                GetComponent<Camera>().orthographicSize = 30;
            }
        }
        else
        {
            //if it's online give the option to log out
            if (NetworkServer.active || NetworkClient.active)
            {
                if (GUI.Button(new Rect(0, 0, 75, 25), "Log Out"))
                {
                    //the camera may be on the player, make sure it's taken off them before the player is removed from the scene
                    GetComponent<Camera>().transform.parent = null;
                    manager.StopHost();
                    //turn off the canvas if it's on
                    mainCanvas.enabled = false;
                }
            }
        }
        if(NetworkServer.active)
        {
            string s = gameController.GetComponent<ConvertIPToRoomCode>().ipToCode(manager.networkAddress);
            GUI.Label(new Rect(20, Screen.height - 25, 300, 25), "Room code: " + s.ToUpper());
        }
    }
}