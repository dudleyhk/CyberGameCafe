using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestStatus : MonoBehaviour {

    private GameObject questStatusUI;

    public GameObject textBoxPrefab;
    public GameObject incompleteIcon;
    public GameObject completeIcon;

    // Comprises a row in the panel. Displays the compleated quests in a list
    public List<QuestStatusInfo> QuestsTracked;

	// Use this for initialization
	void Awake()
    {
        createQuestDisplay();
	}

    void updateDisplay()
    {
        // consider usign if display needs to be updated.
    }

    void createQuestDisplay()
    {
        Debug.Log("Yes I work");
        GameObject textBox;
        int counter = 0;
        foreach(QuestStatusInfo q in QuestsTracked)
        {
            textBox = Instantiate(textBoxPrefab, gameObject.transform);
            textBox.GetComponent<Text>().text = q.QuestName;
            textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -40 + (-30 * (counter)));
            counter++;
        }
    }

    void ToggleCompleationState(string questName)
    {
        for(int i = 0; i < QuestsTracked.Count; i++)
        {
            if(QuestsTracked[i].QuestName == questName)
            {
                QuestsTracked[i].isQuestComplete(true);
            }
        }
    }
}
