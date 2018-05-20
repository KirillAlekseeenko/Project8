using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Task_ReachDestinationPoint : TaskResolver {

	[SerializeField]
	private string checkedObject;

	[SerializeField]
	private Player checkedObjectOwner;

	void OnTriggerEnter(Collider col){
		var temp = col.gameObject.GetComponent (Type.GetType (checkedObject));
		if (temp != null) {
			if ((temp as Unit).Owner == checkedObjectOwner) {
				completeTask ();
			}
		}
	}
}
