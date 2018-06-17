using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hack_Camera : TaskResolver {

	[SerializeField] private WatchingCamera camera;

	private void OnEnable(){
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			yield return new WaitForSeconds (secondsBetweenUpdates);
			if (camera.IsActivated)
				break;
		}
		completeTask ();
		yield return null;
	}
}
