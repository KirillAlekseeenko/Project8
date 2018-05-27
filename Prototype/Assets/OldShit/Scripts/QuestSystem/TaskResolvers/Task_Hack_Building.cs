using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hack_Building : TaskResolver {

	void OnTriggerEnter(Collider col){
		var temp = col.gameObject.GetComponent<Hacker>();
		if (temp != null) {
			completeTask ();
		}
	}
}
