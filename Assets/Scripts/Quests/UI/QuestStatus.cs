using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatus : MonoBehaviour {

 //   public Sprite completeIcon;

 //   // Fields that track the quests and display a UI element in the 
 //   private List<GameObject> questSprites; 
 //   public List<QuestStatusInfo> QuestsTracked;

	//// Use this for initialization
	//void Start()
 //   {
	//}

 //   void createQuestDisplay()
 //   {
 //       questSprites = new List<GameObject>();
 //       Debug.Log("Yes I work");
 //       GameObject textBox;
 //       int counter = 0;
 //       foreach(QuestStatusInfo q in QuestsTracked)
 //       {
 //           textBox = Instantiate(q.gameObject, gameObject.transform);
 //           textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -80 + (-60 * (counter)));
 //           textBox.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
 //           questSprites.Add(textBox);
 //           counter++;
 //       }
 //   }

 //   void OnEnable()
 //   {
 //       if(questSprites == null)
 //       {
 //           createQuestDisplay();
 //       }
 //   }

 //   public void ToggleCompleationState(string questName)
 //   {
 //       for (int i = 0; i < QuestsTracked.Count; i++)
 //       {
 //           if(QuestsTracked[i].QuestName == questName)
 //           {
 //               QuestsTracked[i].isQuestComplete(true);
 //               Destroy(questSprites[i]);

 //               Debug.Log("I Toggled");
 //               break;
 //           }
 //       }
 //   }
}
