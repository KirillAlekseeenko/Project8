using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour { // selection and actions

	[SerializeField]
	private Camera camera;

	HashSet<WorldObject> objectsInsideFrustum;


	HashSet<WorldObject> selectedUnits;

	List<WorldObject> debugList;

	WorldObject currentlyHighlightedObject; // onMouseHover

	private bool isNeutralObjectSelected;

	private bool isShiftDown = false;

	public bool IsShiftDown {
		get {
			return isShiftDown;
		}
		set {
			isShiftDown = value;
		}
	}

	public HashSet<WorldObject> ObjectsInsideFrustum {
		get {
			return objectsInsideFrustum;
		}
	}

	public HashSet<WorldObject> SelectedUnits {
		get {
			return selectedUnits;
		}
	}

	void Awake()
	{
		objectsInsideFrustum = new HashSet<WorldObject> ();
		selectedUnits = new HashSet<WorldObject> ();
	}

	public void OnMouseHover (Vector3 mousePosition)
	{
		var ray = camera.ScreenPointToRay (mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var worldObject = hit.collider.gameObject.GetComponent<WorldObject> ();

			if (worldObject != null && !selectedUnits.Contains(worldObject)) { // НАДО ЧТО-ТО ДЕЛАТЬ С КОДОМ В ЭТОМ IF!!11

				if (!worldObject.IsVisible)
					return;

				if (currentlyHighlightedObject != null && !selectedUnits.Contains(currentlyHighlightedObject)) {
					currentlyHighlightedObject.Dehighlight ();
				}

				worldObject.Highlight ();
				currentlyHighlightedObject = worldObject;

			} else {
				if (currentlyHighlightedObject != null) {
					
					if(!selectedUnits.Contains(currentlyHighlightedObject))
						currentlyHighlightedObject.Dehighlight ();
					
					currentlyHighlightedObject = null;
				}
			}
		} 
	}
	public void OnLeftButtonDown (Vector3 mousePosition)
	{
		var ray = camera.ScreenPointToRay (mousePosition);

		if (!isShiftDown)
			removeSelection ();

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var worldObject = hit.collider.gameObject.GetComponent<WorldObject> ();
			if (worldObject != null && worldObject.IsVisible) {
				selectObject (worldObject);
			}
		}

	}
	public void OnLeftButtonUp (Vector3 mousePosition)
	{
		//Debug.Log (selectedUnits.Count);
	}
	public void OnDrag(Vector3 clickPosition, Vector3 mousePosition)
	{
		foreach (WorldObject worldObject in objectsInsideFrustum) {
			
			if (IsWithinSelectionBounds (worldObject, clickPosition, mousePosition) && worldObject.Owner.IsHuman && worldObject is Unit) {
				if (!worldObject.IsSelected) {
					selectObject (worldObject);
				}
			} else {
				if (worldObject.IsSelected) {
					unselectObject (worldObject);
				}
			}
		}
	}

	private void selectObject(WorldObject worldObject)
	{
		worldObject.IsSelected = true;
		worldObject.Highlight ();
		selectedUnits.Add(worldObject);
	}
	private void unselectObject(WorldObject worldObject)
	{
		worldObject.IsSelected = false;
		worldObject.Dehighlight ();
		selectedUnits.Remove(worldObject);
	}
		


	private void removeSelection()
	{
		List<WorldObject> deletionList = new List<WorldObject>(selectedUnits);
		foreach (WorldObject worldObject in deletionList) {
			unselectObject (worldObject);
		}
	}

	private bool IsWithinSelectionBounds( WorldObject worldObject, Vector3 mousePos1, Vector3 mousePos2 )
	{
		var camera = Camera.main;
		var viewportBounds = UIUtils.GetViewportBounds( camera, mousePos1, mousePos2 );

		return viewportBounds.Contains ( camera.WorldToViewportPoint( worldObject.transform.position ) );
	}

}
