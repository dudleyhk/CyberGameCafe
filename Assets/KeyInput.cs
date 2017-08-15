using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NOTE - Temporary solution to test the dialog system. A Key input system will need to be designed for handaling input. 
public class KeyInput : MonoBehaviour {

    private bool inConversation;
    private DialogEngine NPCDialogObject;

    void Awake()
    {
    }

	// Update is called once per frame
	void Update ()
    {
        if (NPCDialogObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                NPCDialogObject.moveToNode(0);
                Debug.Log("0 key pressed.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                NPCDialogObject.moveToNode(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                NPCDialogObject.moveToNode(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                NPCDialogObject.moveToNode(3);
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "NPC")
        {
            NPCDialogObject = col.gameObject.GetComponent<DialogEngine>();
        }
    }

    public void endConversationState()
    {
        NPCDialogObject = null;
    }
}
