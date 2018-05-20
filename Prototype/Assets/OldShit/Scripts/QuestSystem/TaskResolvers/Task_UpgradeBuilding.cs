using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_UpgradeBuilding : TaskResolver {

	[SerializeField]
	private Building checkedBuilding;

	[SerializeField]
	private int necessaryLevel;

	private void Start(){
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			if (checkedBuilding.CurrentLevel == necessaryLevel)
				break;
			yield return new WaitForSeconds (2);
		}
		completeTask ();
		yield return null;
	}
}
