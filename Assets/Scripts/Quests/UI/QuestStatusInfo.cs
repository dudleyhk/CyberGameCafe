using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusInfo : MonoBehaviour {

    public string QuestName;
    private bool QuestComplete = false;

    public void isQuestComplete(bool value)
    {
        QuestComplete = value;
    }
}
