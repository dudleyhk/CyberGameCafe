using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    public GameObject currentObjectiveUI;
    public GameObject questStatusUI;

    bool paused;

    // Update is called once per frame
    void Start()
    {
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = true;
            GameObject.FindGameObjectWithTag("Player").
            GetComponent<Movement>().stopMovement();

            if (currentObjectiveUI.activeInHierarchy) { currentObjectiveUI.SetActive(false); }

            pauseMenu.SetActive(true);
            if (questStatusUI)
            {
                questStatusUI.SetActive(true);
            }

        }

        if (paused && Input.GetKeyDown(KeyCode.M))
        {
            Destroy(GameObject.Find("Audio(Clone)"));
            Destroy(GameObject.Find("EternalObject"));
            SceneManager.LoadScene("MainMenu");
        }

        if (paused && Input.GetKeyDown(KeyCode.R))
        {
            paused = false;
            GameObject.FindGameObjectWithTag("Player").
                GetComponent<Movement>().startMovement();

            //objectiveUI.SetActive(true);

            if (questStatusUI)
            {
                questStatusUI.SetActive(false);
            }

            pauseMenu.SetActive(false);
            //transform.parent.gameObject.SetActive(false);
        }
    }
}
