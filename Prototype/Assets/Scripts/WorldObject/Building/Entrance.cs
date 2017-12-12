using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	public delegate void EntranceHandler(GameObject unit);
	public event EntranceHandler entranceHandler;

	private Building thisBuilding;

	private void Awake () {
		thisBuilding = gameObject.GetComponentInParent <Building>();
		entranceHandler = new EntranceHandler(thisBuilding.AddUnit);
	}

	private void Update () {
		
	}

	private void OnTriggerEnter(Collider collider){
		if (collider.tag == "Unit") {
			entranceHandler (collider.gameObject);
		}
	}


}
