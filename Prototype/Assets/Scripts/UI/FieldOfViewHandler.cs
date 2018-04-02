using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewHandler : MonoBehaviour {

	public delegate void HideMinimapMark(GameObject unit, bool isHide);
	public static event HideMinimapMark OnUnitHide;
	[SerializeField]
	private GameObject fieldOfViewPrefab;

	private HashSet<Unit> objectsInsideTheFrustum;

	private HashSet<Unit> visibleObjects;

	private bool isAltOn;

	void Awake()
	{
		objectsInsideTheFrustum = new HashSet<Unit> ();
		visibleObjects = new HashSet<Unit> ();
	}

	public GameObject FieldOfViewPrefab {
		get {
			return fieldOfViewPrefab;
		}
	}

	public bool IsAltOn {
		get {
			return isAltOn;
		}
		set {
			foreach (Unit unit in visibleObjects) {
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
		if (!visibleObjects.Contains (unit)) {

			unit.GetComponent<MeshRenderer> ().enabled = true;
			if(OnUnitHide!=null)
				OnUnitHide(unit.gameObject, false);

			if (isAltOn) {
				unit.GetComponent<VisionArcComponent> ().IsTurnedOn = true;
			}
				
			visibleObjects.Add (unit);
		}
	}

	public void Remove(Unit unit)
	{
		if (visibleObjects.Contains (unit)) {

			unit.GetComponent<MeshRenderer> ().enabled = false;
			if(OnUnitHide!=null)
				OnUnitHide(unit.gameObject, true);

			if (isAltOn) {
				unit.GetComponent<VisionArcComponent> ().IsTurnedOn = false;
			}
				
			visibleObjects.Remove (unit);
		}
	}
		
}
