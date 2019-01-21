using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfLevelConditions : MonoBehaviour {

	[SerializeField] private GameObject endPanel;
	[SerializeField] private Text mainText;
	[SerializeField] private Text subText;
	[SerializeField] private Building firstBuilding;

	private RevealGrade revGrade;

	void Awake(){
		revGrade = GameObject.FindObjectOfType<RevealGrade> ();
	}

	void Update(){
		if (QuestManager.questManager.RequestCompleteQuest (QuestManager.questManager.questList.Count)) {
			endPanel.SetActive (true);
			mainText.text = "Победа";
			subText.text = "Вам удалось пройти обучающий уровень";
		} 
		if (!firstBuilding.Owner.IsHuman) {
			endPanel.SetActive (true);
			mainText.text = "Поражение";
			subText.text = "Вы потерпели сокрушительное поражение";
		}
	}
}
