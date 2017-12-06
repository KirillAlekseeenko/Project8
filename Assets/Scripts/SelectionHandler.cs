using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour {

	bool isSelecting = false;
	Vector3 mousePosition1;

	private Collider[] selectableObjects;
	private Unit[] selectedObjects;
	private int selectedObjectsCount = 0;

	public Unit[] SelectedObjects {
		get {
			return selectedObjects;
		}
	}

	void Update()
	{
		// If we press the left mouse button, save mouse location and begin selection
		if( Input.GetMouseButtonDown( 0 ) )
		{
			// multiple selection

			isSelecting = true;
			mousePosition1 = Input.mousePosition;
			var cam = Camera.main;
			var x = Mathf.Tan (cam.fieldOfView * Mathf.Deg2Rad) * cam.farClipPlane; 

			if(selectableObjects != null) 
				foreach(Unit unit in selectedObjects)
				{
					unit.hideHalo ();
					unit.IsSelected = false;
				}

			selectedObjectsCount = 0;
			
			selectableObjects = Physics.OverlapBox (cam.transform.position, new Vector3 (x, x * cam.aspect, cam.farClipPlane), cam.transform.rotation, LayerMask.GetMask("Selectable"));

			// one click select

			var ray = cam.ScreenPointToRay (mousePosition1);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Selectable")){

					hit.collider.gameObject.GetComponent<Unit> ().IsSelected = true;
					hit.collider.gameObject.GetComponent<Unit> ().showHalo ();
					selectedObjectsCount++;
					
				}
			}

		}
		// If we let go of the left mouse button, end selection
		if (Input.GetMouseButtonUp (0)) {
			isSelecting = false;
			selectedObjects = new Unit[selectedObjectsCount];

			int currentIndex = 0;

			foreach (Collider collider in selectableObjects) {
				if(collider.gameObject.GetComponent<Unit>().IsSelected)
					selectedObjects [currentIndex++] = collider.gameObject.GetComponent<Unit>(); // creating array that contains selected units
			}

		}
	}

	void OnGUI()
	{
		if( isSelecting )
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

	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !isSelecting )
			return false;

		var camera = Camera.main;
		var viewportBounds =
			UIUtils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );

		return viewportBounds.Contains(
			camera.WorldToViewportPoint( gameObject.transform.position ) );
	}

}
