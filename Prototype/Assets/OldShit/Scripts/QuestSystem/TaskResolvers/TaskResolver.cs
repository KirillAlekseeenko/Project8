using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskResolver : MonoBehaviour {
	[SerializeField]
	UnityEvent eventWhenTaskComplete;
	[SerializeField] 
	protected int connectedQuestID;
	[SerializeField]
	protected int connectedTaskID;

	protected void completeTask(){
		QuestManager.questManager.CompleteTask (connectedQuestID, connectedTaskID);
		eventWhenTaskComplete.Invoke ();
	}
}


