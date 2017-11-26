using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour {

	[SerializeField]
	private GameObject iconPanel;

	[SerializeField]
	private EventSystem eventSystem;

	bool isSelecting = false;
	private float singleSelectionZone = 20.0f;

	Vector3 mousePosition1;

	private Collider[] selectableObjects;
	private Unit[] selectedObjects;
	private int selectedObjectsCount = 0; // it's puprose - to prevent activating multiple selection when user holds LMB without moving pointer

	public Unit[] SelectedObjects {
		get {
			return selectedObjects;
		}
	}
		

	void Update()
	{
		// If we press the left mouse button, save mouse location and begin selection
		if( Input.GetMouseButtonDown( 0 ) && !eventSystem.IsPointerOverGameObject())
		{
			// multiple selection

			isSelecting = true;
			mousePosition1 = Input.mousePosition;
			var cam = Camera.main;
			var x = Mathf.Tan (cam.fieldOfView * Mathf.Deg2Rad) * cam.farClipPlane; 

			resetSelection ();
			
			selectableObjects = Physics.OverlapBox (cam.transform.position, new Vector3 (x, x * cam.aspect, cam.farClipPlane), cam.transform.rotation, LayerMask.GetMask("Selectable"));

			// one click select

			var ray = cam.ScreenPointToRay (mousePosition1);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Selectable")){

					selectOneUnit (hit.collider.gameObject.GetComponent<Unit> ());
					
				}
			}

		}
		// If we let go of the left mouse button, end selection
		if (Input.GetMouseButtonUp (0) && !eventSystem.IsPointerOverGameObject()) {
			isSelecting = false;
			confirmSelection ();

		}
	}

	void OnGUI()
	{
		if( isSelecting && Vector3.Distance(mousePosition1, Input.mousePosition) > singleSelectionZone)
		{
			// Create a rect from both mouse positions
			var rect = UIUtils.GetScreenRect( mousePosition1, Input.mousePosition );
			UIUtils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			UIUtils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );

			selectedObjectsCount = 0;

			foreach (Collider collider in selectableObjects) {
				if (IsWithinSelectionBounds (collider.gameObject)) {
					collider.gameObject.GetComponent<Unit> ().showHalo ();
					collider.gameObject.GetComponent<Unit> ().IsSelected = true;
					selectedObjectsCount++;
				} else {
					collider.gameObject.GetComponent<Unit> ().hideHalo ();
					collider.gameObject.GetComponent<Unit> ().IsSelected = false;
				}

			}


		}

	}

	private void showUnitImages ()
	{
		foreach (Unit unit in selectedObjects) {
			unit.Icon = Instantiate (unit.IconPrefab, iconPanel.transform);
			unit.Icon.GetComponent<UnitIcon> ().Unit = unit;
			unit.Icon.GetComponent<UnitIcon> ().SelectionHandler = GetComponent<SelectionHandler> ();
		}
	}
	private void resetSelection()
	{
		if (selectedObjects != null) {
			foreach (Unit unit in selectedObjects) {
				unit.hideHalo ();
				unit.IsSelected = false;
				Destroy (unit.Icon);
			}
		}

		selectedObjectsCount = 0;
	}

	public void selectOneUnit(Unit unit)
	{
		resetSelection ();
		unit.IsSelected = true;
		unit.showHalo ();
		selectedObjectsCount++;
	}
	public void confirmSelection()
	{
		selectedObjects = new Unit[selectedObjectsCount];

		int currentIndex = 0;

		foreach (Collider collider in selectableObjects) {
			if(collider.gameObject.GetComponent<Unit>().IsSelected)
				selectedObjects [currentIndex++] = collider.gameObject.GetComponent<Unit>(); // creating array that contains selected units
		}

		showUnitImages ();
	}


	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !isSelecting )
			return false;

		var camera = Camera.main;
		var viewportBounds = UIUtils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );

		return viewportBounds.Contains ( camera.WorldToViewportPoint( gameObject.transform.position ) );
	}

}
