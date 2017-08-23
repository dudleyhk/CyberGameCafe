using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkToNPC : MonoBehaviour
{
    private GameObject player;

    private GameObject interButton;

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

        textBox = GameObject.FindGameObjectWithTag("TextBox");
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
                textBox.GetComponent<DialogueMessages>().spawnTextBox("Someone has left a load of USB"
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

                textBox.GetComponent<DialogueMessages>().spawnTextBox
                    ("Thank you so much, this university would be nothing without you.\n\nQUEST COMPLETE!");

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
                textBox.GetComponent<DialogueMessages>().spawnTextBox("I would have done it myself but I"
                + " don't know how to get out from behind this desk");
            }
        }
    }
}