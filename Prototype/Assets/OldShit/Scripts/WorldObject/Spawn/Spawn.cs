using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	// Use this for initialization

	[SerializeField] private Transform movePoint;

	private Unit movedUnit;
	private bool moving;

	public void SpawnUnit(Unit unit)
	{
		movedUnit = Instantiate (unit.gameObject, transform.position, Quaternion.identity).GetComponent<Unit>();
		moving = true;
	}

	void Update(){
		if (moving) {
			if (Vector3.Distance (movedUnit.transform.position, movePoint.position) > 0.1) {
				movedUnit.GetComponent<Rigidbody> ().MovePosition (movePoint.position);
				moving = false;
			}
		}
	}
}
