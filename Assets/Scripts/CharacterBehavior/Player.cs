using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player Manager class.
/// </summary>
public class Player : MonoBehaviour
{
    public bool _loggedIn = false;
    public GameObject npcElement = null;
    public NPCMovement npcMovement = null;
    public ushort spawnNodeID = 0;
   

    /// <summary>
    /// Use to get and set _loggedIn also turn on and off the NPCElement based on 
    ///     the current state of _loggedIn. 
    /// </summary>
    public bool LoggedIn
    {
        get
        {
            return _loggedIn;
        }

        set
        {
            _loggedIn = value;
            if(_loggedIn)
            {
                npcElement.SetActive(false);
            }
            else
            {
                npcElement.SetActive(true);
            }
        }
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            LoggedIn = !LoggedIn;
        }


        // DEBUGGING
        if (_loggedIn)
        {
            npcElement.SetActive(false);
        }
        else
        {
            npcElement.SetActive(true);
        }
    }



}
