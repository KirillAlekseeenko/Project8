using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	public delegate bool EntranceHandler(Unit unit);
	public event EntranceHandler entranceHandler;
	public event EntranceHandler invasion;

	private Building thisBuilding;

	private List<GameObject> unitsWithin;

	private void Awake () {
		thisBuilding = gameObject.GetComponentInParent <Building>();
		entranceHandler = new EntranceHandler(thisBuilding.AddUnit);
		invasion = new EntranceHandler (thisBuilding.InvadeUnit);

		unitsWithin = new List<GameObject> ();
	}

	/*private void OnTriggerEnter(Collider collider){
		
		if (collider.gameObject.GetComponent<Unit> () != null) {
			if (collider.gameObject.GetComponent<Unit> ().Owner !=
				gameObject.GetComponentInParent<Building>().Owner) {

				invasion (collider.gameObject.GetComponent<Unit>());
			}
			else
				entranceHandler (collider.gameObject.GetComponent<Unit>());
		}
		unitsWithin.Add (collider.gameObject);
	}*/

	public bool UnitWithin(Unit unit){
		if (unitsWithin.Contains (unit.gameObject)) {
			unitsWithin.Remove (unit.gameObject);
			return true;
		} else {
			return false;
		}
	}

	public Vector3 Coordinates { get { return gameObject.transform.position; } }
}
