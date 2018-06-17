using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_UpgradeBuilding : TaskResolver {

	[SerializeField]
	private Building checkedBuilding;

	[SerializeField]
	private int necessaryLevel;

	private void OnEnable(){
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			yield return new WaitForSeconds (secondsBetweenUpdates);
			if (checkedBuilding.CurrentLevel >= necessaryLevel)
				break;
		}
		completeTask ();
		yield return null;
	}
}
