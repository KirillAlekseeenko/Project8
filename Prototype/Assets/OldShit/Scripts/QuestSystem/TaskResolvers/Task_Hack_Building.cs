using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hack_Building : TaskResolver {

	private ControlPanel controlPanel;

	private void OnEnable(){
		controlPanel = gameObject.GetComponent<ControlPanel> (); 
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			yield return new WaitForSeconds (secondsBetweenUpdates);
			if (controlPanel.IsActivated)
				break;
		}
		completeTask ();
		yield return null;
	}
}
