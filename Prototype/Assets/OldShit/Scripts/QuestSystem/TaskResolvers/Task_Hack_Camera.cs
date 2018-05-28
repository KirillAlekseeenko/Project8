using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hack_Camera : TaskResolver {

	private WatchingCamera camera;

	private void Start(){
		camera = gameObject.GetComponent<WatchingCamera> (); 
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			if (camera.IsActivated)
				break;
			yield return new WaitForSeconds (2);
		}
		completeTask ();
		yield return null;
	}
}
