using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject playerPrefab = null;
    public List<GameObject> players = new List<GameObject>();
    


    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.A))
        {
            print("Create New Player");
            SpawnNewPlayer();
        }
    }


    /// <summary>
    /// Create a new player into the scene at a specified spawn node. 
    /// </summary>
    public void SpawnNewPlayer()
    {
        GameObject player = Instantiate(playerPrefab, this.transform);
        player.GetComponent<Player>().spawnNodeID = GridManager.Instance.spawnNodeID;


        player.name = "Player Character " + players.Count;

        Vector3 nodePosition = GridManager.Instance.GetNode(GridManager.Instance.spawnNodeID).Centre;
        Vector3 playerPosition = new Vector3(nodePosition.x, nodePosition.y, 0f);
        player.transform.position = playerPosition;


        players.Add(player);
    }

}
