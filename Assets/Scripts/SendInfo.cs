using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendInfo : NetworkBehaviour
{
    [SyncVar]
    public int score;

    void Start()
    {
        score = 0;
    }
}
