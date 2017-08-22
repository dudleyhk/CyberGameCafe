using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkToNPC : MonoBehaviour
{
    private GameObject player;

    private GameObject interButton;

    [SerializeField]
    private GameObject textBox;

    [SerializeField]
    private GameObject collectible;
    private GameObject[] collectibleClone;

    private bool playerInBox = false;

    void Start()
    {
        collectibleClone = new GameObject[15];
        interButton = GameObject.FindGameObjectWithTag("InteractButton");
        interButton.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            playerInBox = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (col.gameObject == player)
        {
            playerInBox = false;
        }
    }

    void TaskOnClick()
    {
        if (playerInBox)
        {
            string currentObj = player.GetComponent<QuestSystem>().
                getActiveMission().getActiveObjective().objectiveTag;
            //print("Current objective: " + currentObj);


            if (currentObj == "talkToMan")
            {
                //show a dialogue box
                spawnTextBox("Someone has left a load of USB"
                    + " sticks around campus which are infected with malware.\n"
                    + "If anyone finds one of them and plugs it into their"
                    + " computer it could spell disaster.\nPlease can you "
                    + "find them and bring them back here.");
                
                //spawn the sticks
                for (int i = 0; i < 15; i++)
                {
                    collectibleClone[i] = Instantiate(collectible);

                    //get a random space from all the accessible spaces on the map
                    SetupMap m = GameObject.Find("Map").GetComponent<SetupMap>();
                    var id = -1;
                    do
                    {
                        id = Random.Range(0, m.grid.Length - 1);
                    } while (SetupMap.nodeGraph.nodes[id].solid == true);

                    //spawn a USB stick there
                    var node = SetupMap.nodeGraph.nodes[id];
                    collectibleClone[i].transform.position = node.position;
                    collectibleClone[i].GetComponent<CollectibleObject>().questTag = "getUsb";
                }

                //update quest
                player.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "talkToMan");

            }

            else if (currentObj == "handInQuest")
            {

                spawnTextBox("Thank you so much, this university would be nothing without you.\n\n"
                + "QUEST COMPLETE!");

                //destroy all the leftover USB sticks
                for(int i = 0; i < 15; i++)
                {
                    if(collectibleClone[i] != null)
                    {
                        Destroy(collectibleClone[i]);
                    }
                }

                //update quest
                player.GetComponent<QuestSystem>().updateMissionState(MissionObjectiveTypes.OBJ_EVENT, "handInQuest");
            }

            else
            {
                spawnTextBox("I would have done it myself but I"
                + " don't know how to get out from behind this desk");
            }
        }
    }

    void spawnTextBox(string text)
    {
        //hide the UI
        showOrHideUI(false);

        //show a dialogue box
        textBox.GetComponent<Button>().onClick.AddListener(advanceText);
        textBox.GetComponentInChildren<Text>().text = text;
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponent<Button>().enabled = true;
        textBox.GetComponentInChildren<Text>().enabled = true;
    }

    void advanceText()
    {
        //show UI again
        showOrHideUI(true);
        
        //destroy the text box
        textBox.GetComponent<Image>().enabled = false;
        textBox.GetComponent<Button>().enabled = false;
        textBox.GetComponentInChildren<Text>().enabled = false;
    }

    void showOrHideUI(bool b)
    {
        //Debug.Log(b ? "Enabling the things" : "Disabling the things");

        interButton.GetComponent<Button>().enabled = b;
        interButton.GetComponent<Image>().enabled = b;
        interButton.GetComponentInChildren<Text>().enabled = b;

        GameObject js = GameObject.FindGameObjectWithTag("Joystick");
        js.GetComponent<VirtualJoysticks>().enabled = b;
        js.GetComponent<Image>().enabled = b;

        GameObject jsChild = js.transform.GetChild(0).gameObject;
        jsChild.GetComponent<Image>().enabled = b;
        jsChild.GetComponentInChildren<Text>().enabled = b;
    }
}