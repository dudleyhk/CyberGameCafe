using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendInfoToServer : NetworkBehaviour
{
    private int score = 0;
    public int newScore = 0;

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        if(newScore != score)
        {
            CmdEditScore();
        }
    }

    [Command]
    void CmdEditScore()
    {
        score = newScore;
    }
}
