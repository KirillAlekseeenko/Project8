using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public enum QuestProgress{
	NOT_AVAILABLE,
	AVAILABLE,
	ACCEPTED,
	COMPLETE
};

[System.Serializable]
public class Quest{
	public int id;
	public string title;
	public QuestProgress questProgress;
	public int nextQuest;

	public List<Task> tasks;
}
