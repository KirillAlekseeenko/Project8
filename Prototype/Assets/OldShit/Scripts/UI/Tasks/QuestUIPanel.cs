using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIPanel : MonoBehaviour {

	public GameObject questPrefab;
	public GameObject taskPrefab;

	public Text dialogueText;

	private List<GameObject> questButtons;
	private List<GameObject> taskButtons;

	private AudioSource audio;

	private void Awake(){
		questButtons = new List<GameObject> ();
		taskButtons = new List<GameObject> ();
	
		audio = GetComponent<AudioSource> ();
	}

	public void RenewInfo(){

		for (int i = 0; i < questButtons.Count; i++)
			Destroy (questButtons [i]);
		questButtons.Clear ();

		for (int i = 0; i < taskButtons.Count; i++)
			Destroy (taskButtons [i]);
		taskButtons.Clear ();

		foreach (Quest quest in QuestManager.questManager.currentQuestsList) {
			GameObject q = Instantiate(questPrefab, parent:gameObject.transform) as GameObject;
			q.GetComponentInChildren<Text> ().text = quest.title;
			questButtons.Add (q);
			foreach (Task task in quest.tasks) {
				GameObject t = Instantiate(taskPrefab, parent:gameObject.transform) as GameObject;
				t.GetComponentInChildren<Text> ().text = task.title;
				t.GetComponent<Button> ().onClick.AddListener (() => OnTaskClick(task.audioExplanation, task.explanation));
				taskButtons.Add (t);
				if (task.taskProgress != QuestProgress.COMPLETE) {
					t.GetComponentsInChildren<Image> ()[1].gameObject.SetActive (false);
				}
			}
		}
	}

	public void OnTaskClick(AudioClip clip, string explanation){
		audio.PlayOneShot (clip);
		dialogueText.text = explanation;
	}
}
