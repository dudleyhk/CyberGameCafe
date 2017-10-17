using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeTrigger : MonoBehaviour
{

    public GameObject[] mazes = new GameObject[4];

    public GameObject gameEndTrigger;

    public GameObject mazeRunner;
    private GameObject mazeRunnerClone;
    private GameObject currentObjective;

    private GameObject player;
    private GameObject newMaze;

    private bool atEnd = false;

    private int counter = 0;

    private void MazeStart(GameObject thePlayer)
    {
        player = thePlayer;
        MazeManager(true);
    }

    public void MazeManager(bool create)
    {
        if (create == true)
        {
            player.GetComponent<Movement>().stopMovement();

            mazeRunnerClone = Instantiate(mazeRunner);
            mazeRunnerClone.transform.position = new Vector3(0, 0, 0);
            mazeRunnerClone.transform.position = new Vector3(39.6f, 36.6f, -2f);

            newMaze = Instantiate(mazes[counter]);
            newMaze.transform.position = gameObject.transform.position;
            counter++;
        }
        else
        {
            //destroy
            Destroy(newMaze);
            player.GetComponent<Movement>().startMovement();
        }
    }

    


    public void StartOrEndTriggered(GameObject thePlayer)
    {
        if (atEnd == true)
        {
            GameObject scorer = GameObject.Find("EternalObject");
            if (scorer)
            {
                EternalScript e = scorer.GetComponent<EternalScript>();
                e.publicPCScore = float.Parse(GameObject.FindGameObjectWithTag("Timer")
                    .GetComponent<Text>().text);
            }

            Application.LoadLevel("SinglePlayer");
        }
        if (atEnd == false)
        {
            MazeStart(thePlayer);
            atEnd = true;
        }
    }
}
