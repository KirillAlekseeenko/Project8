using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumCollider : MonoBehaviour {

	[SerializeField]
	SelectionHandler selectionHandler;

	void OnTriggerEnter(Collider other)
	{
		var worldObject = other.gameObject.GetComponent<WorldObject> ();
			if (worldObject != null) {
			selectionHandler.ObjectsInsideFrustum.Add (worldObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		var worldObject = other.gameObject.GetComponent<WorldObject> ();
		if (worldObject != null) {
			selectionHandler.ObjectsInsideFrustum.Remove (worldObject);
		}
	}

}
