using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientGUI : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 25), "Logout"))
        {
            Network.Disconnect(250);
            SceneManager.LoadScene("MainScene");
        }
    }
}