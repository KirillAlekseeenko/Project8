using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	public delegate bool EntranceHandler(Unit unit);
	public event EntranceHandler entranceHandler;
	public event EntranceHandler invasion;

	private Building thisBuilding;

	private void Start(){
		thisBuilding = gameObject.GetComponentInParent <Building>();
		entranceHandler = new EntranceHandler(thisBuilding.AddUnit);
		invasion = new EntranceHandler (thisBuilding.InvadeUnit);
	}

	public bool UnitWithin(Unit unit){
		if (Vector3.Distance (unit.gameObject.transform.position, gameObject.transform.position) <= 2f)
			return true;
		else
			return false;
	}

	public Vector3 Coordinates { get { return gameObject.transform.position; } }
}
