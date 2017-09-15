using UnityEngine;
using System.Collections;

public class NPCSpawn : MonoBehaviour
{
    public GameObject[] charBody = new GameObject[4];
    public GameObject[] charEyes = new GameObject[5];
    public GameObject[] charTops = new GameObject[4];
    public GameObject[] charTrousers = new GameObject[2];
    public GameObject[] charHair = new GameObject[5];
    public GameObject charShoes;

    public void render(int body, int top, int legs, int eyes, int hair, Color skinC, Color topC, Color legsC, Color shoesC, Color hairC)
    {
        //spawn each component of a character sprite and assign it a colour
        int bodyType = body;
        GameObject newBody = Instantiate(charBody[bodyType], transform, false);
        newBody.GetComponent<Renderer>().material.color = skinC;

        GameObject newTop = Instantiate(charTops[top], newBody.transform, false) as GameObject;
        newTop.GetComponent<Renderer>().material.color = topC;

        GameObject newTrousers = Instantiate(charTrousers[legs], newBody.transform, false) as GameObject;
        newTrousers.GetComponent<Renderer>().material.color = legsC;

        GameObject newShoes = Instantiate(charShoes, newBody.transform, false) as GameObject;
        newShoes.GetComponent<Renderer>().material.color = shoesC;

        Object newEyes = Instantiate(charEyes[eyes], newBody.transform, false);

        //if the character model is compatible with hair/hats spawn one of those too
        if (bodyType == 0)
        {
            GameObject newHair = Instantiate(charHair[hair], newBody.transform, false) as GameObject;
            newHair.GetComponent<Renderer>().material.color = hairC;
        }
    }
}