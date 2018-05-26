using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_ClickOnObject : TaskResolver {

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
			completeTask ();
		}
	}
}
