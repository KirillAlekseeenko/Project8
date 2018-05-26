using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour {

	public static QuestManager questManager;

	[SerializeField]
	private UnityEvent RenewQuestUiInfo;

	public List<Quest> questList = new List<Quest>();
	public List<Quest> currentQuestsList = new List<Quest>();

	void Awake(){
		if (questManager == null)
			questManager = this;
		else if (questManager != this)
			Destroy (gameObject);
	
	}

	void Start(){
		//When the level begins, we add the main level quest to the list of current quests
		AcceptQuest (questList [0].id);	
	}

	private void CheckChainQuest(int questID){
		int tempID = 0;
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].nextQuest > 0) {
				tempID = questList [i].nextQuest;
				break;
			}
		}
		if (tempID > 0) {
			//Unlock the next quest
			for(int i = 0; i < questList.Count; i++){
				if (questList [i].id == tempID && questList [i].questProgress == QuestProgress.NOT_AVAILABLE) {
					questList [i].questProgress = QuestProgress.AVAILABLE;
					AcceptQuest (tempID);
					break;
				}
			}
		}
	}

	private void RenewAfterTime(){
		RenewQuestUiInfo.Invoke ();
	}

	public void AcceptQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].questProgress == QuestProgress.AVAILABLE) {
				currentQuestsList.Add (questList [i]);
				questList [i].questProgress = QuestProgress.ACCEPTED;
				RenewQuestUiInfo.Invoke ();
				break;
			}
		}
	}
	public void GiveUpQuest(int questID){
		for (int i = 0; i < currentQuestsList.Count; i++) {
			if (currentQuestsList [i].id == questID && currentQuestsList [i].questProgress == QuestProgress.ACCEPTED) {
				currentQuestsList [i].questProgress = QuestProgress.AVAILABLE;
				//currentQuestsList [i].questObjectiveCount = 0;
				currentQuestsList.Remove (currentQuestsList[i]);
				break;
			}

		}
	}
	public void CompleteQuest(int questID){
		for (int i = 0; i < currentQuestsList.Count; i++) {
			if (currentQuestsList [i].id == questID 
				&& currentQuestsList [i].questProgress == QuestProgress.ACCEPTED
				&& currentQuestsList [i].tasks.TrueForAll(x => x.taskProgress == QuestProgress.COMPLETE)) {

				currentQuestsList [i].questProgress = QuestProgress.COMPLETE;
				currentQuestsList.Remove (currentQuestsList[i]);
				//Check for chain quest
				CheckChainQuest (questID);
				//RenewQuestUiInfo.Invoke ();
				Invoke ("RenewAfterTime", 10f);
				break;
			}
		}
	}

	public void CompleteTask(int questID, int taskID){
		for (int i = 0; i < currentQuestsList.Count; i++) {
			if (currentQuestsList [i].id == questID) {
				for (int j = 0; j < currentQuestsList [i].tasks.Count; j++) {
					if (currentQuestsList [i].tasks [j].id == taskID 
						&& currentQuestsList [i].tasks [j].taskProgress == QuestProgress.ACCEPTED) {
					
						currentQuestsList [i].tasks [j].taskProgress = QuestProgress.COMPLETE;

						//Check if we complete the entire quest
						CompleteQuest (questID);
						RenewQuestUiInfo.Invoke ();
						return;
					}
				}
			}
		}
	}

	//Requests
	public bool RequestAvailableQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].questProgress == QuestProgress.AVAILABLE)
				return true;
		}
		return false;
	}
	public bool RequestAcceptedQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].questProgress == QuestProgress.ACCEPTED)
				return true;
		}
		return false;
	}
	public bool RequestCompleteQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].questProgress == QuestProgress.COMPLETE)
				return true;
		}
		return false;
	}
}
