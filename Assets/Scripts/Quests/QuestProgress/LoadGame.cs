using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGame : MonoBehaviour
{
	StreamReader fileReader;
	StreamWriter fileWriter;

	public int numberOfQuestsInGame = 4;
	public int numberOfTags = 8;

	[SerializeField]
	Mission[] quests = new Mission[4];
	[SerializeField]
	GameObject[] qg = new GameObject[4];

	QuestSystem questHandler;

	public void loadGame()
	{
		questHandler = GameObject.FindGameObjectWithTag ("Player").GetComponent<QuestSystem>();

		fileReader = new StreamReader ("assets\\scripts\\quests\\questprogress\\questprogress.txt");

		for (int i = 0; i < numberOfQuestsInGame; i++)
		{
			if (fileReader.ReadLine () == "True") {
				questHandler.assignMission (quests[i], qg[i]);
			}
			for (int j = 0; j < quests [i].missionObjectives.Length; j++)
			{
				string tag = fileReader.ReadLine ();
				if (fileReader.ReadLine() == "True")
				{
					questHandler.updateMissionState
					(MissionObjectiveTypes.OBJ_EVENT, tag, true);
				}
			}
		}

		string s = fileReader.ReadLine ();
		float x = (s == null) ? -11 : getFloat(s, 0f);
		s = fileReader.ReadLine ();
		float y = (s == null) ? 9 : getFloat(s, 0f);
		float z = getFloat(fileReader.ReadLine(), 0f);

		Vector3 playerLoc = new Vector3 (x, y, z);

		GameObject.FindGameObjectWithTag ("Player").transform.position = playerLoc;

		fileReader.Close ();
	}

	float getFloat(string s, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse (s, out result);
		return result;
	}

	void OnApplicationQuit()
	{
		writeToTextFile ();
	}
		
	public void writeToTextFile()
	{
		fileWriter = new StreamWriter ("assets\\scripts\\quests\\questprogress\\questprogress.txt");

		for(int i = 0; i < numberOfQuestsInGame; i++)
		{
			fileWriter.WriteLine(questHandler.missionStarted(quests[i]) || quests[i].isCompleated());
			for (int j = 0; j < quests [i].missionObjectives.Length; j++)
			{
				MissionObjective objective = quests [i].missionObjectives [j].GetComponent<MissionObjective>();
				fileWriter.WriteLine(objective.objectiveTag);
				fileWriter.WriteLine (objective.getComplete());
			}
		}

		fileWriter.WriteLine (GameObject.FindGameObjectWithTag
			("Player").transform.position.x);
		fileWriter.WriteLine (GameObject.FindGameObjectWithTag
			("Player").transform.position.y);
		fileWriter.WriteLine (GameObject.FindGameObjectWithTag
			("Player").transform.position.z);

		fileWriter.Close ();
	}
}