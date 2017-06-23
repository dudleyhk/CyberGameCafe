using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;




public class CustomNetworkManager : MonoBehaviour
{
    public GameObject playerPrefab = null;


    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerID)
    {
        var player = GameObject.Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player, playerID);
    }
}
