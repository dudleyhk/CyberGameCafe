//TODO:
//Create preset colours for all parts and have one selected

using UnityEngine;
using System.Collections;

public class SpawnCharacter : MonoBehaviour
{
    private GameObject characterHolder;

    public GameObject[] charBody = new GameObject[4];
    public GameObject[] charEyes = new GameObject[6];
    public GameObject[] charTops = new GameObject[4];
    public GameObject[] charTrousers = new GameObject[2];
    public GameObject[] charShoes = new GameObject[1];
    public GameObject[] charHair = new GameObject[5];
    
    void Start()
    {
        characterHolder = GameObject.FindGameObjectWithTag("Controller");

        int bodyType = Random.Range(0, charBody.Length);
        GameObject newBody = Instantiate(charBody[bodyType], characterHolder.transform, false);
        newBody.GetComponent<Renderer>().material.color = new Color
            (Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
        newBody.transform.position = new Vector3(0,0,0);

        GameObject newTop = Instantiate(charTops[Random.Range(0, charTops.Length)], newBody.transform, false) as GameObject;
        newTop.GetComponent<Renderer>().material.color = new Color
            (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        GameObject newTrousers = Instantiate(charTrousers[Random.Range(0, charTrousers.Length)], newBody.transform, false) as GameObject;
        newTrousers.GetComponent<Renderer>().material.color = new Color
            (Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.8f), 1);

        GameObject newShoes = Instantiate(charShoes[Random.Range(0, charShoes.Length)], newBody.transform, false) as GameObject;
        newShoes.GetComponent<Renderer>().material.color = new Color
            (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        Object newEyes = Instantiate(charEyes[Random.Range(0, charEyes.Length)], newBody.transform, false);

        //if the character model is compatible with hair/hats spawn one of those too
        if (bodyType == 0)
        {
            GameObject newHair = Instantiate(charHair[Random.Range(0, charHair.Length)], newBody.transform, false) as GameObject;
            newHair.GetComponent<Renderer>().material.color = new Color
                (Random.Range(0f, 1f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), 1);
        }
    }
}