using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class OnEnterTrigger : MonoBehaviour {

	public UnityEvent EnterEvent;
	public UnityEvent LeaveEvent;

	[SerializeField] private string collidedObject;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.GetComponent (Type.GetType (collidedObject)) != null)
			EnterEvent.Invoke ();
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.GetComponent (Type.GetType (collidedObject)) != null)
			LeaveEvent.Invoke ();
	}
}
