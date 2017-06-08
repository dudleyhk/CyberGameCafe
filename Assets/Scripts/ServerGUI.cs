using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerGUI : MonoBehaviour
{
    void OnGUI()
    {
        for (int i = 0; i < Network.connections.Length; i++)
        {
            GUI.Label(new Rect(0, 30 + (i * 25), 120, 25), "Player " + (i + 1) + " Score: 0");
        }
        
        GUI.Label(new Rect(120, 0, 100, 25), "Connections: " + Network.connections.Length);

        if (GUI.Button(new Rect(0, 0, 100, 25), "Logout"))
        {
            Network.Disconnect(250);
            SceneManager.LoadScene("MainScene");
        }
    }
}