using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;



public class Menu : MonoBehaviour
{
    public string IP = "164.11.111.82";
    public int port = 25001;

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 25), "Start Client"))
        {
            Network.Connect(IP, port);
        }
        if (GUI.Button(new Rect(100, 125, 100, 25), "Start Server"))
        {
            Network.InitializeServer(9, port, false);
            SceneManager.LoadScene("ServerScene");
        }
    }

    void OnConnectedToServer()
    {
        SceneManager.LoadScene("ClientScene");
    }
    void OnDisconnectedFromServer()
    {
    }
}