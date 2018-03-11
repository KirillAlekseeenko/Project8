using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	public delegate void EntranceHandler(GameObject unit);
	public event EntranceHandler entranceHandler;
	public event EntranceHandler invasion;

	private Building thisBuilding;

	private void Awake () {
		thisBuilding = gameObject.GetComponentInParent <Building>();
		entranceHandler = new EntranceHandler(thisBuilding.AddUnit);
		invasion = new EntranceHandler (thisBuilding.InvadeUnit);
	}

	private void Update () {

	}

	private void OnTriggerEnter(Collider collider){
		if (collider.gameObject.GetComponent<Unit> () != null) {
			if (collider.gameObject.GetComponent<Unit> ().Owner !=
				gameObject.GetComponentInParent<Building>().Owner) {

				invasion (collider.gameObject);
			}
			else
				entranceHandler (collider.gameObject);
		}
	}

	public Vector3 Coordinates { get { return gameObject.transform.position; } }
}
