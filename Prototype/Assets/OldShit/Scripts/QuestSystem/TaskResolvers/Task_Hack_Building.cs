using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hack_Building : TaskResolver {

	private ControlPanel controlPanel;

	private void Start(){
		controlPanel = gameObject.GetComponent<ControlPanel> (); 
		StartCoroutine (checkCondition());
	}

	protected IEnumerator checkCondition(){
		while(true){
			if (controlPanel.IsActivated)
				break;
			yield return new WaitForSeconds (2);
		}
		completeTask ();
		yield return null;
	}
}
