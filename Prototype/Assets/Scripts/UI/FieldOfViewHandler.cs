using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewHandler : MonoBehaviour {

	private HashSet<Unit> objectsInsideTheFrustum;

	private bool isAltOn;

	void Awake()
	{
		objectsInsideTheFrustum = new HashSet<Unit> ();
	}

	public bool IsAltOn {
		get {
			return isAltOn;
		}
		set {
			foreach (Unit unit in objectsInsideTheFrustum) {
				if (value) {

					unit.GetComponent<VisionArcComponent> ().IsTurnedOn = true;
				} else {

					unit.GetComponent<VisionArcComponent> ().IsTurnedOn = false;
				}
			}
			isAltOn = value;
		}
	}

	public void Add(Unit unit)
	{
		if (isAltOn) {
			unit.GetComponent<VisionArcComponent> ().IsTurnedOn = true;
		}

		objectsInsideTheFrustum.Add (unit);
	}

	public void Remove(Unit unit)
	{
		if (isAltOn) {
			unit.GetComponent<VisionArcComponent> ().IsTurnedOn = false;
		}

		objectsInsideTheFrustum.Remove (unit);
	}
}
