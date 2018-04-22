using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_CaptureBuilding : TaskResolver {

	[SerializeField]
	private Building checkedBuilding;

	[SerializeField]
	private Player checkedOwner;

	private void Start(){
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			if (checkedBuilding.Owner == checkedOwner)
				break;
			yield return new WaitForSeconds (2);
		}
		completeTask ();
		yield return null;
	}

}
