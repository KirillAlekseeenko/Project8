using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskResolver : MonoBehaviour {
	[SerializeField]
	UnityEvent eventWhenTaskComplete;
	[SerializeField]
	protected float secondsBetweenUpdates;

	protected int connectedQuestID;
	protected int connectedTaskID;

	protected void completeTask(){
		QuestManager.questManager.CompleteTask (connectedQuestID, connectedTaskID);
		eventWhenTaskComplete.Invoke ();
	}

	public void ConnectedTask(int questID, int taskID){
		connectedQuestID = questID;
		connectedTaskID = taskID;
	}
}


