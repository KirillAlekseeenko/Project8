using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hack_Camera : TaskResolver {

	[SerializeField] private ControlPanel cameraControlBox;

	private void OnEnable(){
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			yield return new WaitForSeconds (secondsBetweenUpdates);
			if (cameraControlBox.IsActivated)
				break;
		}
		completeTask ();
		yield return null;
	}
}
