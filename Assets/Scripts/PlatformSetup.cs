/*
    Based on which platform this game is running will depend on the display and options available
 
 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformSetup : MonoBehaviour
{

    private void Awake()
    {
#if UNITY_STANDALONE // Any standalone platform (Win, Unix, Mac)



#elif UNITY_ANDROID // Any android device


#else
        Debug.Log("Platform not recognised");
#endif
    }
}
