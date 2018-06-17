using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_CaptureBuilding : TaskResolver {

	[SerializeField]
	private Building checkedBuilding;

	[SerializeField]
	private Player checkedOwner;

	private void OnEnable(){
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			yield return new WaitForSeconds (secondsBetweenUpdates);
			if (checkedBuilding.Owner == checkedOwner)
				break;
		}
		completeTask ();
		yield return null;
	}

}
